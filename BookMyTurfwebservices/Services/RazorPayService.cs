using Microsoft.Extensions.Options;
using Razorpay.Api;
using BookMyTurfwebservices.Models.DTOs.Responses;
using BookMyTurfwebservices.Models.Settings;
using BookMyTurfwebservices.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BookMyTurfwebservices.Services;

public class RazorPayService : IRazorPayService
{
    private readonly RazorpayClient _razorpayClient;
    private readonly PaymentGatewaySettings _settings;
    private readonly ILogger<RazorPayService> _logger;

    public RazorPayService(
        IOptions<PaymentGatewaySettings> settings,
        ILogger<RazorPayService> logger)
    {
        _settings = settings.Value;
        _logger = logger;

        _razorpayClient = new RazorpayClient(
            _settings.Razorpay.KeyId,
            _settings.Razorpay.KeySecret);
    }

    public async Task<RazorpayOrder> CreateOrderAsync(
        decimal amount,
        string receipt,
        string customerEmail,
        string customerPhone,
        string? description = null)
    {
        try
        {
            var options = new Dictionary<string, object>
            {
                { "amount", (int)(amount * 100) }, // Convert to paise
                { "currency", "INR" },
                { "receipt", receipt },
                { "payment_capture", 1 }
            };

            // Add customer information if available
            if (!string.IsNullOrEmpty(customerEmail) || !string.IsNullOrEmpty(customerPhone))
            {
                var notes = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(customerEmail))
                    notes.Add("customer_email", customerEmail);
                if (!string.IsNullOrEmpty(customerPhone))
                    notes.Add("customer_phone", customerPhone);
                if (!string.IsNullOrEmpty(description))
                    notes.Add("description", description);

                options.Add("notes", notes);
            }

            // Razorpay SDK is synchronous
            var razorpayOrder = _razorpayClient.Order.Create(options);

            // Proper logging - cast dynamic to string
            var orderId = razorpayOrder["id"].ToString();
            //_logger.LogInformation("Created Razorpay order: {OrderId}", orderId);

            return new RazorpayOrder
            {
                Id = orderId!,
                Amount = amount,
                Currency = razorpayOrder["currency"].ToString()!,
                Status = razorpayOrder["status"].ToString()!,
                CreatedAt = DateTimeOffset.FromUnixTimeSeconds(
                    Convert.ToInt64(razorpayOrder["created_at"])).DateTime
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create Razorpay order for receipt: {Receipt}", receipt);
            throw new RazorPayException("Failed to create payment order", ex);
        }
    }

    public async Task<PaymentVerificationResult> VerifyPaymentAsync(
        string paymentId,
        string orderId,
        string signature)
    {
        try
        {
            // Fetch payment details
            var razorpayPayment = _razorpayClient.Payment.Fetch(paymentId);

            var paymentStatus = razorpayPayment["status"].ToString();
            if (paymentStatus != "captured")
            {
                return new PaymentVerificationResult
                {
                    IsValid = false,
                    Message = $"Payment status is {paymentStatus}",
                    GatewayResponse = razorpayPayment.ToString()!
                };
            }

            // Verify signature using custom implementation
            var isValid = VerifyPaymentSignature(orderId, paymentId, signature, _settings.Razorpay.KeySecret);

            return new PaymentVerificationResult
            {
                IsValid = isValid,
                Message = isValid ? "Payment verified successfully" : "Invalid payment signature",
                PaymentMethod = razorpayPayment["method"].ToString()!,
                GatewayResponse = razorpayPayment.ToString()!
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to verify payment {PaymentId}", paymentId);

            return new PaymentVerificationResult
            {
                IsValid = false,
                Message = "Payment verification failed",
                GatewayResponse = ex.Message
            };
        }
    }

    public async Task<RefundResult> ProcessRefundAsync(
    string paymentId,
    decimal amount,
    string reason)
    {
        try
        {
            // Convert amount to paise
            var amountInPaise = (int)(amount * 100);

            // Create refund request - Razorpay SDK expects amount in options
            var options = new Dictionary<string, object>
        {
            { "amount", amountInPaise },
            { "speed", "normal" }
        };

            if (!string.IsNullOrEmpty(reason))
            {
                var notes = new Dictionary<string, string> { { "reason", reason } };
                options.Add("notes", notes);
            }

            // FIXED: Create a Payment object first, then call Refund on it
            var payment = _razorpayClient.Payment.Fetch(paymentId);

            // Call Refund method on the Payment object
            var refund = payment.Refund(options);

            var refundId = refund["id"].ToString();
            //_logger.LogInformation("Processed refund {RefundId} for payment {PaymentId}",
            //    refundId, paymentId);

            return new RefundResult
            {
                Success = true,
                RefundId = Guid.NewGuid().ToString(), // Your internal refund ID
                GatewayRefundId = refundId!,
                Amount = amount,
                Status = refund["status"].ToString()!,
                GatewayResponse = refund.ToString()!
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process refund for payment {PaymentId}", paymentId);

            return new RefundResult
            {
                Success = false,
                RefundId = Guid.NewGuid().ToString(),
                GatewayRefundId = string.Empty,
                Amount = amount,
                Status = "failed",
                GatewayResponse = ex.Message,
                ErrorMessage = ex.Message
            };
        }
    }

    public bool ValidateWebhookSignature(WebhookEvent webhookEvent)
    {
        try
        {
            var payload = webhookEvent.Payload.ToString();
            var secret = _settings.Razorpay.WebhookSecret;
            var receivedSignature = webhookEvent.Signature;

            var generatedSignature = GenerateHmacSha256(payload!, secret);
            return receivedSignature == generatedSignature;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Webhook signature validation failed");
            return false;
        }
    }

    public string GetKeyId()
    {
        return _settings.Razorpay.KeyId;
    }

    private bool VerifyPaymentSignature(string orderId, string paymentId, string signature, string secret)
    {
        try
        {
            var message = orderId + "|" + paymentId;
            var generatedSignature = GenerateHmacSha256(message, secret);
            return signature == generatedSignature;
        }
        catch
        {
            return false;
        }
    }

    private string GenerateHmacSha256(string message, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}

public class RazorPayException : Exception
{
    public RazorPayException(string message) : base(message) { }
    public RazorPayException(string message, Exception innerException)
        : base(message, innerException) { }
}