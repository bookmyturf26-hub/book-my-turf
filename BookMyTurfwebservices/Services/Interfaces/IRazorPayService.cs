using BookMyTurfwebservices.Models.DTOs.Responses;

namespace BookMyTurfwebservices.Services.Interfaces;

public interface IRazorPayService
{
    Task<RazorpayOrder> CreateOrderAsync(
        decimal amount,
        string receipt,
        string customerEmail,
        string customerPhone,
        string? description = null);

    Task<PaymentVerificationResult> VerifyPaymentAsync(
        string paymentId,
        string orderId,
        string signature);

    Task<RefundResult> ProcessRefundAsync(
        string paymentId,
        decimal amount,
        string reason);

    bool ValidateWebhookSignature(WebhookEvent webhookEvent);
    string GetKeyId();
}