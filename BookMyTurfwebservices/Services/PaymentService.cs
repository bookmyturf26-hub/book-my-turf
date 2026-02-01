using BookMyTurfwebservices.Data;
using BookMyTurfwebservices.Models.DTOs.Requests;
using BookMyTurfwebservices.Models.DTOs.Responses;
using BookMyTurfwebservices.Models.Entities;
using BookMyTurfwebservices.Models.Enums;
using BookMyTurfwebservices.Services.Interfaces;
using BookMyTurfwebservices.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BookMyTurfwebservices.Services;

public class PaymentService : IPaymentService
{
    private readonly ApplicationDbContext _context;
    private readonly IRazorPayService _razorPayService;
    private readonly ILogger<PaymentService> _logger;
    private readonly IIdempotencyChecker _idempotencyChecker;
    private readonly IEmailService _emailService;

    public PaymentService(
        ApplicationDbContext context,
        IRazorPayService razorPayService,
        ILogger<PaymentService> logger,
        IIdempotencyChecker idempotencyChecker,
        IEmailService emailService)
    {
        _context = context;
        _razorPayService = razorPayService;
        _logger = logger;
        _idempotencyChecker = idempotencyChecker;
        _emailService = emailService;
    }

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        // Check idempotency
        var idempotencyKey = $"create_order_{request.BookingId}_{request.Amount}";
        if (await _idempotencyChecker.IsDuplicateRequestAsync(idempotencyKey))
        {
            var existingPayment = await _context.Payments
                .Where(p => p.BookingId == request.BookingId &&
                           p.Status == PaymentStatus.Pending)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync();

            if (existingPayment != null)
            {
                return MapToOrderResponse(existingPayment);
            }
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Check for existing pending payment
            var existingPendingPayment = await _context.Payments
                .FirstOrDefaultAsync(p =>
                    p.BookingId == request.BookingId &&
                    p.Status == PaymentStatus.Pending &&
                    p.CreatedAt > DateTime.UtcNow.AddMinutes(-10));

            if (existingPendingPayment != null)
            {
                return MapToOrderResponse(existingPendingPayment);
            }

            // Create order with payment gateway
            var razorpayOrder = await _razorPayService.CreateOrderAsync(
                request.Amount,
                $"booking_{request.BookingId}",
                request.CustomerEmail,
                request.CustomerPhone,
                request.Description);

            // Save payment record
            var payment = new Payment
            {
                Id = Guid.NewGuid().ToString(),
                BookingId = request.BookingId,
                OrderId = razorpayOrder.Id,
                Amount = request.Amount,
                Status = PaymentStatus.Pending,
                CustomerEmail = request.CustomerEmail,
                CustomerPhone = request.CustomerPhone,
                CreatedAt = DateTime.UtcNow,
                Notes = request.Description
            };

            await _context.Payments.AddAsync(payment);

            // Create initial transaction
            var transactionRecord = new PaymentTransaction
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = payment.Id,
                TransactionType = TransactionType.OrderCreation,
                Amount = request.Amount,
                Status = "created",
                GatewayTransactionId = razorpayOrder.Id,
                CreatedAt = DateTime.UtcNow
            };

            await _context.PaymentTransactions.AddAsync(transactionRecord);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Mark idempotency
            await _idempotencyChecker.MarkRequestAsProcessedAsync(idempotencyKey);

            _logger.LogInformation("Created payment order {OrderId} for booking {BookingId}",
                razorpayOrder.Id, request.BookingId);

            return MapToOrderResponse(payment, razorpayOrder);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Failed to create order for booking {BookingId}",
                request.BookingId);
            throw new PaymentServiceException("Failed to create payment order", ex);
        }
    }

    public async Task<PaymentVerificationResponse> VerifyPaymentAsync(VerifyPaymentRequest request)
    {
        // Check idempotency
        var idempotencyKey = $"verify_payment_{request.PaymentId}";
        if (await _idempotencyChecker.IsDuplicateRequestAsync(idempotencyKey))
        {
            var existingPayment = await _context.Payments
                .Include(p => p.Transactions)
                .FirstOrDefaultAsync(p => p.TransactionId == request.PaymentId);

            if (existingPayment != null && existingPayment.Status == PaymentStatus.Completed)
            {
                return new PaymentVerificationResponse
                {
                    IsValid = true,
                    Message = "Payment already verified",
                    BookingId = existingPayment.BookingId,
                    PaymentStatus = PaymentStatus.Completed,
                    PaymentId = existingPayment.TransactionId,
                    Amount = existingPayment.Amount
                };
            }
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Verify with payment gateway
            var verification = await _razorPayService.VerifyPaymentAsync(
                request.PaymentId,
                request.OrderId,
                request.Signature);

            if (!verification.IsValid)
            {
                await UpdatePaymentStatusAsync(
                    request.OrderId,
                    PaymentStatus.Failed,
                    verification.Message);

                return new PaymentVerificationResponse
                {
                    IsValid = false,
                    Message = verification.Message,
                    PaymentStatus = PaymentStatus.Failed
                };
            }

            // Get payment from database
            var payment = await _context.Payments
                .Include(p => p.Transactions)
                .FirstOrDefaultAsync(p => p.OrderId == request.OrderId);

            if (payment == null)
            {
                throw new InvalidOperationException($"Payment not found for order {request.OrderId}");
            }

            // Check if already processed
            if (payment.Status == PaymentStatus.Completed)
            {
                return new PaymentVerificationResponse
                {
                    IsValid = true,
                    Message = "Payment already verified",
                    BookingId = payment.BookingId,
                    PaymentStatus = PaymentStatus.Completed,
                    PaymentId = payment.TransactionId,
                    Amount = payment.Amount
                };
            }

            // Update payment status
            payment.Status = PaymentStatus.Completed;
            payment.TransactionId = request.PaymentId;
            payment.PaidAt = DateTime.UtcNow;
            payment.PaymentMethod = verification.PaymentMethod;
            payment.GatewayResponse = verification.GatewayResponse;

            // Create transaction record
            var transactionRecord = new PaymentTransaction
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = payment.Id,
                TransactionType = TransactionType.Payment,
                Amount = payment.Amount,
                Status = "captured",
                GatewayTransactionId = request.PaymentId,
                GatewayResponse = verification.GatewayResponse,
                CreatedAt = DateTime.UtcNow
            };

            await _context.PaymentTransactions.AddAsync(transactionRecord);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Mark idempotency
            await _idempotencyChecker.MarkRequestAsProcessedAsync(idempotencyKey);

            // Send notification
            await SendPaymentConfirmationAsync(payment);

            _logger.LogInformation("Verified payment {PaymentId} for booking {BookingId}",
                request.PaymentId, payment.BookingId);

            return new PaymentVerificationResponse
            {
                IsValid = true,
                Message = "Payment verified successfully",
                BookingId = payment.BookingId,
                PaymentStatus = PaymentStatus.Completed,
                PaymentId = payment.TransactionId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaidAt = payment.PaidAt
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Payment verification failed for order {OrderId}",
                request.OrderId);
            throw new PaymentServiceException("Payment verification failed", ex);
        }
    }

    public async Task<RefundResponse> ProcessRefundAsync(RefundRequestDto request)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Get payment
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == request.PaymentId || p.TransactionId == request.PaymentId);

            if (payment == null)
            {
                throw new InvalidOperationException($"Payment not found: {request.PaymentId}");
            }

            if (payment.Status != PaymentStatus.Completed)
            {
                throw new InvalidOperationException($"Cannot refund payment with status: {payment.Status}");
            }

            // Process refund with payment gateway
            var refundResult = await _razorPayService.ProcessRefundAsync(
                payment.TransactionId!,
                request.Amount,
                request.Reason);

            if (!refundResult.Success)
            {
                throw new InvalidOperationException($"Refund failed: {refundResult.ErrorMessage}");
            }

            // Update payment status
            payment.Status = request.Amount == payment.Amount
                ? PaymentStatus.Refunded
                : PaymentStatus.PartiallyRefunded;

            payment.RefundAmount = request.Amount;
            payment.RefundReason = request.Reason;
            payment.RefundedAt = DateTime.UtcNow;

            // Create refund transaction
            var refundTransaction = new PaymentTransaction
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = payment.Id,
                TransactionType = request.Amount == payment.Amount
                    ? TransactionType.FullRefund
                    : TransactionType.PartialRefund,
                Amount = request.Amount,
                Status = "refunded",
                GatewayTransactionId = refundResult.RefundId,
                GatewayResponse = refundResult.GatewayResponse,
                Notes = request.Reason,
                CreatedAt = DateTime.UtcNow,
                ProcessedAt = DateTime.UtcNow
            };

            await _context.PaymentTransactions.AddAsync(refundTransaction);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Send refund notification
            if (!string.IsNullOrEmpty(payment.CustomerEmail))
            {
                await _emailService.SendRefundNotificationAsync(
                    payment.CustomerEmail,
                    payment.TransactionId!,
                    request.Amount,
                    request.Reason,
                    DateTime.UtcNow);
            }

            _logger.LogInformation("Processed refund {RefundId} for payment {PaymentId}",
                refundResult.RefundId, payment.Id);

            return new RefundResponse
            {
                Success = true,
                RefundId = refundTransaction.Id,
                PaymentId = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                RefundAmount = request.Amount,
                Status = "processed",
                GatewayRefundId = refundResult.RefundId,
                RequestedAt = DateTime.UtcNow,
                ProcessedAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Refund failed for payment {PaymentId}",
                request.PaymentId);
            throw new PaymentServiceException("Refund processing failed", ex);
        }
    }

    public async Task HandlePaymentWebhookAsync(WebhookEvent webhookEvent)
    {
        try
        {
            // Process webhook based on event type
            switch (webhookEvent.EventType)
            {
                case "payment.captured":
                    await HandlePaymentCapturedWebhook(webhookEvent);
                    break;

                case "payment.failed":
                    await HandlePaymentFailedWebhook(webhookEvent);
                    break;

                case "refund.processed":
                    await HandleRefundProcessedWebhook(webhookEvent);
                    break;

                default:
                    _logger.LogInformation("Unhandled webhook event type: {EventType}",
                        webhookEvent.EventType);
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing webhook event {EventId}",
                webhookEvent.EventId);
            throw;
        }
    }

    public async Task<PaymentStatusResponse> GetPaymentStatusAsync(string paymentId)
    {
        var payment = await _context.Payments
            .Include(p => p.Transactions)
            .FirstOrDefaultAsync(p => p.Id == paymentId ||
                                     p.TransactionId == paymentId ||
                                     p.OrderId == paymentId);

        if (payment == null)
        {
            throw new InvalidOperationException($"Payment not found: {paymentId}");
        }

        var latestTransaction = payment.Transactions
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefault();

        return new PaymentStatusResponse
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            TransactionId = payment.TransactionId,
            Amount = payment.Amount,
            Status = payment.Status.ToString(),
            PaymentMethod = payment.PaymentMethod,
            CreatedAt = payment.CreatedAt,
            PaidAt = payment.PaidAt,
            RefundAmount = payment.RefundAmount,
            RefundedAt = payment.RefundedAt,
            LatestTransaction = latestTransaction != null ? new TransactionInfo
            {
                Type = latestTransaction.TransactionType.ToString(),
                Status = latestTransaction.Status,
                CreatedAt = latestTransaction.CreatedAt
            } : null
        };
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByBookingIdAsync(string bookingId)
    {
        return await _context.Payments
            .Include(p => p.Transactions)
            .Where(p => p.BookingId == bookingId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    private async Task UpdatePaymentStatusAsync(
        string orderId,
        PaymentStatus status,
        string? message = null)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(p => p.OrderId == orderId);

        if (payment != null)
        {
            payment.Status = status;
            if (message != null)
            {
                payment.GatewayResponse = message;
            }
            await _context.SaveChangesAsync();
        }
    }

    private async Task SendPaymentConfirmationAsync(Payment payment)
    {
        try
        {
            if (!string.IsNullOrEmpty(payment.CustomerEmail) && payment.PaidAt.HasValue)
            {
                await _emailService.SendPaymentConfirmationAsync(
                    payment.CustomerEmail,
                    payment.BookingId,
                    payment.Amount,
                    payment.TransactionId!,
                    payment.PaidAt.Value);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send payment confirmation email for booking {BookingId}",
                payment.BookingId);
        }
    }

    private async Task HandlePaymentCapturedWebhook(WebhookEvent webhookEvent)
    {
        _logger.LogInformation("Handling payment captured webhook for event {EventId}",
            webhookEvent.EventId);

        // Implement webhook processing logic here
        // For example: Update payment status in database
    }

    private async Task HandlePaymentFailedWebhook(WebhookEvent webhookEvent)
    {
        _logger.LogInformation("Handling payment failed webhook for event {EventId}",
            webhookEvent.EventId);

        // Implement webhook processing logic here
    }

    private async Task HandleRefundProcessedWebhook(WebhookEvent webhookEvent)
    {
        _logger.LogInformation("Handling refund processed webhook for event {EventId}",
            webhookEvent.EventId);

        // Implement webhook processing logic here
    }

    private OrderResponse MapToOrderResponse(Payment payment, RazorpayOrder? razorpayOrder = null)
    {
        return new OrderResponse
        {
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Currency = "INR",
            KeyId = _razorPayService.GetKeyId(),
            CreatedAt = payment.CreatedAt,
            Status = "created",
            PaymentId = payment.Id,
            BookingId = payment.BookingId
        };
    }

    private OrderResponse MapToOrderResponse(Payment payment)
    {
        return new OrderResponse
        {
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Currency = "INR",
            KeyId = _razorPayService.GetKeyId(),
            CreatedAt = payment.CreatedAt,
            Status = "created",
            PaymentId = payment.Id,
            BookingId = payment.BookingId
        };
    }
}

public class PaymentServiceException : Exception
{
    public PaymentServiceException(string message) : base(message) { }
    public PaymentServiceException(string message, Exception innerException)
        : base(message, innerException) { }
}