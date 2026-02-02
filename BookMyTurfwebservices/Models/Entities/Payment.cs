using System.ComponentModel.DataAnnotations;
using BookMyTurfwebservices.Models.Enums;
using BookMyTurfwebservices.Models.Interfaces;

namespace BookMyTurfwebservices.Models.Entities;

public class Payment : IAuditable, ISoftDeletable
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(36)]
    public string BookingId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string OrderId { get; set; } = string.Empty;

    [StringLength(100)]
    public string? TransactionId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [StringLength(3)]
    public string Currency { get; set; } = "INR"; // Added this

    [StringLength(100)]
    public string? CustomerEmail { get; set; } // Added this

    [StringLength(15)]
    public string? CustomerPhone { get; set; } // Added this

    [StringLength(2000)]
    public string? GatewayResponse { get; set; }

    public decimal? RefundAmount { get; set; } // Added this

    [StringLength(500)]
    public string? RefundReason { get; set; } // Added this

    [StringLength(1000)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? PaidAt { get; set; }

    public DateTime? RefundedAt { get; set; } // Added this

    public DateTime? ExpiresAt { get; set; }

    // Navigation properties
    public virtual ICollection<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();
    public virtual ICollection<RefundRequestEntity> RefundRequests { get; set; } = new List<RefundRequestEntity>();

    // IAuditable
    public string? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; set; }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}