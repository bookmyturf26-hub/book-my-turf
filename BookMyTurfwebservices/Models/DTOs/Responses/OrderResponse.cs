namespace BookMyTurfwebservices.Models.DTOs.Responses;

public class OrderResponse
{
    public string OrderId { get; set; }
    public string PaymentId { get; set; }
    public string BookingId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "INR";
    public string KeyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Notes { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerPhone { get; set; }
}

public class RazorpayOrder
{
    public string Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}