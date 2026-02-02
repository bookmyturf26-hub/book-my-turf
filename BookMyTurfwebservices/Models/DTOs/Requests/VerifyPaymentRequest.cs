using System.ComponentModel.DataAnnotations;

namespace BookMyTurfwebservices.Models.DTOs.Requests;

public class VerifyPaymentRequest
{
    [Required]
    [StringLength(100)]
    public string OrderId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string PaymentId { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Signature { get; set; } = string.Empty;
}