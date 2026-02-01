using Newtonsoft.Json.Linq;

namespace BookMyTurfwebservices.Models.DTOs.Responses;

public class WebhookEvent
{
    public string EventId { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public JObject Payload { get; set; } = new JObject();
    public string Signature { get; set; } = string.Empty;
    public Dictionary<string, string>? Headers { get; set; }
}