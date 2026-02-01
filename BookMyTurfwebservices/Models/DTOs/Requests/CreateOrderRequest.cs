using System.ComponentModel.DataAnnotations;

namespace BookMyTurfwebservices.Models.DTOs.Requests;

public class CreateOrderRequest
{
    [Required]
    [StringLength(36, MinimumLength = 36)]
    public string BookingId { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 100000)]
    public decimal Amount { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(15)]
    public string CustomerPhone { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Description { get; set; }
}