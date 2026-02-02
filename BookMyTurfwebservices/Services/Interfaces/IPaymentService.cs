using BookMyTurfwebservices.Models.DTOs.Requests;
using BookMyTurfwebservices.Models.DTOs.Responses;
using BookMyTurfwebservices.Models.Entities;

namespace BookMyTurfwebservices.Services.Interfaces;

public interface IPaymentService
{
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request);
    Task<PaymentVerificationResponse> VerifyPaymentAsync(VerifyPaymentRequest request);
    Task<RefundResponse> ProcessRefundAsync(RefundRequestDto request);
    Task HandlePaymentWebhookAsync(WebhookEvent webhookEvent);
    Task<PaymentStatusResponse> GetPaymentStatusAsync(string paymentId);
    Task<IEnumerable<Payment>> GetPaymentsByBookingIdAsync(string bookingId);
}