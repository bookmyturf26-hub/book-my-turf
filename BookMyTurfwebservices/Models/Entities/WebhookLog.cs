using System.ComponentModel.DataAnnotations;

namespace BookMyTurfwebservices.Models.Entities;

public class WebhookLog
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(100)]
    public string EventId { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string EventType { get; set; } = string.Empty;

    [Required]
    public string Payload { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Signature { get; set; }

    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ProcessedAt { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "received";

    [StringLength(2000)]
    public string? Error { get; set; }

    [StringLength(1000)]
    public string? Headers { get; set; }
}