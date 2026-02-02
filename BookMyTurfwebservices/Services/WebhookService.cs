using Microsoft.EntityFrameworkCore;
using BookMyTurfwebservices.Data;
using BookMyTurfwebservices.Models.Entities;
using BookMyTurfwebservices.Models.DTOs.Responses;
using BookMyTurfwebservices.Services.Interfaces;
using Newtonsoft.Json;

namespace BookMyTurfwebservices.Services;

public class WebhookService : IWebhookService
{
    private readonly ApplicationDbContext _context;
    private readonly IPaymentService _paymentService;
    private readonly IRazorPayService _razorPayService;
    private readonly ILogger<WebhookService> _logger;

    public WebhookService(
        ApplicationDbContext context,
        IPaymentService paymentService,
        IRazorPayService razorPayService,
        ILogger<WebhookService> logger)
    {
        _context = context;
        _paymentService = paymentService;
        _razorPayService = razorPayService;
        _logger = logger;
    }

    public async Task ProcessWebhookAsync(WebhookEvent webhookEvent)
    {
        // Validate webhook signature first
        if (!_razorPayService.ValidateWebhookSignature(webhookEvent))
        {
            _logger.LogWarning("Invalid webhook signature for event {EventId}", webhookEvent.EventId);
            return;
        }

        // Check if webhook already processed
        var existingLog = await _context.WebhookLogs
            .FirstOrDefaultAsync(w => w.EventId == webhookEvent.EventId);

        if (existingLog != null)
        {
            _logger.LogInformation("Webhook {EventId} already processed", webhookEvent.EventId);
            return;
        }

        // Log webhook receipt
        var webhookLog = new WebhookLog
        {
            Id = Guid.NewGuid().ToString(),
            EventId = webhookEvent.EventId,
            EventType = webhookEvent.EventType,
            Payload = JsonConvert.SerializeObject(webhookEvent.Payload),
            Signature = webhookEvent.Signature,
            ReceivedAt = DateTime.UtcNow,
            Status = "received",
            Headers = webhookEvent.Headers != null ? JsonConvert.SerializeObject(webhookEvent.Headers) : null
        };

        await _context.WebhookLogs.AddAsync(webhookLog);
        await _context.SaveChangesAsync();

        try
        {
            // Process webhook based on event type
            switch (webhookEvent.EventType)
            {
                case "payment.captured":
                case "payment.failed":
                case "refund.processed":
                    await _paymentService.HandlePaymentWebhookAsync(webhookEvent);
                    break;

                default:
                    _logger.LogWarning("Unhandled webhook event type: {EventType}", webhookEvent.EventType);
                    webhookLog.Status = "unhandled";
                    break;
            }

            // Update webhook log status
            webhookLog.Status = "processed";
            webhookLog.ProcessedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Processed webhook {EventId} successfully", webhookEvent.EventId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process webhook {EventId}", webhookEvent.EventId);

            // Update webhook log with error
            webhookLog.Status = "failed";
            webhookLog.Error = ex.Message;
            await _context.SaveChangesAsync();

            throw;
        }
    }

    public async Task<WebhookLog> GetWebhookLogAsync(string eventId)
    {
        var webhookLog = await _context.WebhookLogs
            .FirstOrDefaultAsync(w => w.EventId == eventId);

        if (webhookLog == null)
        {
            throw new InvalidOperationException($"Webhook log not found for event: {eventId}");
        }

        return webhookLog;
    }

    public async Task<IEnumerable<WebhookLog>> GetWebhookLogsAsync(DateTime fromDate, DateTime toDate)
    {
        return await _context.WebhookLogs
            .Where(w => w.ReceivedAt >= fromDate && w.ReceivedAt <= toDate)
            .OrderByDescending(w => w.ReceivedAt)
            .ToListAsync();
    }
}