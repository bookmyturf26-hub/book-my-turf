namespace BookMyTurfwebservices.Services.Interfaces;

public interface IEmailService
{
    Task SendPaymentConfirmationAsync(
        string email,
        string bookingId,
        decimal amount,
        string transactionId,
        DateTime paidAt);

    Task SendRefundNotificationAsync(
        string email,
        string transactionId,
        decimal amount,
        string reason,
        DateTime refundedAt);

    Task SendBookingConfirmationAsync(
        string email,
        string bookingId,
        string turfName,
        DateTime bookingDate,
        string timeSlot);
}