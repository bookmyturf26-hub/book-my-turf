using System.ComponentModel.DataAnnotations;
using BookMyTurfwebservices.Models.Enums;

namespace BookMyTurfwebservices.Models.Entities;

public class PaymentTransaction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(36)]
    public string PaymentId { get; set; } = string.Empty;

    [Required]
    public TransactionType TransactionType { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = string.Empty;

    [StringLength(100)]
    public string? GatewayTransactionId { get; set; }

    [StringLength(2000)]
    public string? GatewayResponse { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ProcessedAt { get; set; }

    // Navigation property
    public virtual Payment? Payment { get; set; }
}