using BookMyTurfwebservices.Data;
using BookMyTurfwebservices.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BookMyTurfwebservices.Models.Entities;

public class BookingSyncLog : IAuditable
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(36)]
    public string BookingId { get; set; }

    [Required]
    [StringLength(36)]
    public string PaymentId { get; set; }

    [Required]
    [StringLength(20)]
    public string SyncType { get; set; } // "CREATE", "UPDATE", "CANCEL"

    [Required]
    public string RequestPayload { get; set; }

    public string? ResponsePayload { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } // "PENDING", "SUCCESS", "FAILED", "RETRY"

    [StringLength(500)]
    public string? ErrorMessage { get; set; }

    public int RetryCount { get; set; }

    public DateTime? NextRetryAt { get; set; }

    public DateTime? ProcessedAt { get; set; }

    // IAuditable
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; set; }
}