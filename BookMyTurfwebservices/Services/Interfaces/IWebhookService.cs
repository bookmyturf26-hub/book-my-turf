using BookMyTurfwebservices.Models.DTOs.Responses;
using BookMyTurfwebservices.Models.Entities;

namespace BookMyTurfwebservices.Services.Interfaces;

public interface IWebhookService
{
    Task ProcessWebhookAsync(WebhookEvent webhookEvent);
    Task<WebhookLog> GetWebhookLogAsync(string eventId);
    Task<IEnumerable<WebhookLog>> GetWebhookLogsAsync(DateTime fromDate, DateTime toDate);
}