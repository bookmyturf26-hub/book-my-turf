using BookMyTurfwebservices.Models.Settings;
using BookMyTurfwebservices.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BookMyTurfwebservices.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IOptions<EmailSettings> emailSettings,
        ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendPaymentConfirmationAsync(
        string email,
        string bookingId,
        decimal amount,
        string transactionId,
        DateTime paidAt)
    {
        var subject = $"Payment Confirmation - Booking #{bookingId}";
        var body = $@"
            <html>
            <body>
                <h2>Payment Confirmation</h2>
                <p>Dear Customer,</p>
                <p>Your payment has been successfully processed.</p>
                
                <h3>Payment Details:</h3>
                <ul>
                    <li><strong>Booking ID:</strong> {bookingId}</li>
                    <li><strong>Transaction ID:</strong> {transactionId}</li>
                    <li><strong>Amount:</strong> ₹{amount:N2}</li>
                    <li><strong>Payment Date:</strong> {paidAt:dd MMM yyyy HH:mm}</li>
                </ul>
                
                <p>Thank you for choosing BookMyTurf!</p>
                
                <p>Best regards,<br>
                BookMyTurf Team</p>
            </body>
            </html>";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendRefundNotificationAsync(
        string email,
        string transactionId,
        decimal amount,
        string reason,
        DateTime refundedAt)
    {
        var subject = $"Refund Processed - Transaction #{transactionId}";
        var body = $@"
            <html>
            <body>
                <h2>Refund Notification</h2>
                <p>Dear Customer,</p>
                <p>Your refund has been processed successfully.</p>
                
                <h3>Refund Details:</h3>
                <ul>
                    <li><strong>Original Transaction ID:</strong> {transactionId}</li>
                    <li><strong>Refund Amount:</strong> ₹{amount:N2}</li>
                    <li><strong>Refund Date:</strong> {refundedAt:dd MMM yyyy HH:mm}</li>
                    <li><strong>Reason:</strong> {reason}</li>
                </ul>
                
                <p>The amount will be credited to your account within 5-7 business days.</p>
                
                <p>Best regards,<br>
                BookMyTurf Team</p>
            </body>
            </html>";

        await SendEmailAsync(email, subject, body);
    }

    public async Task SendBookingConfirmationAsync(
        string email,
        string bookingId,
        string turfName,
        DateTime bookingDate,
        string timeSlot)
    {
        var subject = $"Booking Confirmation - #{bookingId}";
        var body = $@"
            <html>
            <body>
                <h2>Booking Confirmed!</h2>
                <p>Dear Customer,</p>
                <p>Your booking has been confirmed successfully.</p>
                
                <h3>Booking Details:</h3>
                <ul>
                    <li><strong>Booking ID:</strong> {bookingId}</li>
                    <li><strong>Turf:</strong> {turfName}</li>
                    <li><strong>Date:</strong> {bookingDate:dd MMM yyyy}</li>
                    <li><strong>Time Slot:</strong> {timeSlot}</li>
                </ul>
                
                <p>Please arrive 15 minutes before your scheduled time.</p>
                
                <p>Best regards,<br>
                BookMyTurf Team</p>
            </body>
            </html>";

        await SendEmailAsync(email, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(
                    _emailSettings.SenderEmail,
                    _emailSettings.SenderPassword),
                EnableSsl = _emailSettings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);

            _logger.LogInformation("Email sent successfully to {Email}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Email}", toEmail);
            throw;
        }
    }
}