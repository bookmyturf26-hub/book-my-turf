using BookMyTurfwebservices.Models.Enums;

namespace BookMyTurfwebservices.Models.DTOs.Responses;

public class PaymentVerificationResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; }
    public string? BookingId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string? PaymentId { get; set; }
    public string? OrderId { get; set; }
    public decimal? Amount { get; set; }
    public string? Currency { get; set; } = "INR";
    public string? PaymentMethod { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? GatewayResponse { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

public class PaymentVerificationResult
{
    public bool IsValid { get; set; }
    public string Message { get; set; }
    public string? PaymentMethod { get; set; }
    public string GatewayResponse { get; set; }
    public Dictionary<string, object>? AdditionalData { get; set; }
}