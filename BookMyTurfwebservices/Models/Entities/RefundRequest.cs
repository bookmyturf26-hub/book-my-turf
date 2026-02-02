using BookMyTurfwebservices.Data;
using BookMyTurfwebservices.Models.Enums;
using BookMyTurfwebservices.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BookMyTurfwebservices.Models.Entities;

public class RefundRequest : IAuditable, ISoftDeletable
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(36)]
    public string PaymentId { get; set; }

    [Required]
    public string RefundId { get; set; }

    [Required]
    [StringLength(36)]
    public string BookingId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public decimal RefundAmount { get; set; }

    [Required]
    public string Currency { get; set; } = "INR";

    [Required]
    public RefundStatus Status { get; set; } = RefundStatus.Pending;

    [Required]
    public RefundType Type { get; set; } = RefundType.Full;

    [Required]
    [StringLength(500)]
    public string Reason { get; set; }

    public RefundReasonCode ReasonCode { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    [StringLength(100)]
    public string? GatewayRefundId { get; set; }

    [StringLength(1000)]
    public string? GatewayResponse { get; set; }

    public RefundSpeed Speed { get; set; } = RefundSpeed.Normal;

    public RefundInitiatedBy InitiatedBy { get; set; } = RefundInitiatedBy.Merchant;

    [StringLength(100)]
    public string? InitiatedByUser { get; set; }

    public bool SendNotification { get; set; } = true;

    public DateTime? EstimatedSettlementDate { get; set; }

    public DateTime? RefundedAt { get; set; }

    // Foreign key navigation
    public virtual Payment Payment { get; set; }

    // IAuditable
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; set; }

    // ISoftDeletable
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}