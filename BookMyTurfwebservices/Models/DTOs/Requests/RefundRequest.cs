using System.ComponentModel.DataAnnotations;

namespace BookMyTurfwebservices.Models.DTOs.Requests;

public class RefundRequest
{
    [Required]
    [StringLength(36, MinimumLength = 36)]
    public string PaymentId { get; set; }

    [Required]
    [Range(0.01, 100000)]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string Reason { get; set; }

    [StringLength(100)]
    public string? ReferenceNumber { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? CustomerEmail { get; set; }

    [StringLength(20)]
    public string? CustomerPhone { get; set; }
    public bool SendNotification { get; internal set; }
}
public class PartialRefundRequest
{
    [Required]
    [StringLength(36, MinimumLength = 36)]
    public string PaymentId { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 100000)]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string Reason { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(100)]
    public string? CustomerEmail { get; set; }

    [StringLength(20)]
    public string? CustomerPhone { get; set; }

    public bool SendNotification { get; set; } = true;
}

public class FullRefundRequest
{
    [Required]
    [StringLength(36, MinimumLength = 36)]
    public string PaymentId { get; set; } = string.Empty;

    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string Reason { get; set; } = string.Empty;

    public bool SendNotification { get; set; } = true;
}