namespace BookMyTurfwebservices.Models.DTOs.Responses;

public class RefundResult
{
    public bool Success { get; set; }
    public string RefundId { get; set; }
    public string GatewayRefundId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string GatewayResponse { get; set; }
    public string? ErrorMessage { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

public class RefundEligibilityResponse
{
    public bool IsEligible { get; set; }
    public string PaymentId { get; set; }
    public decimal PaymentAmount { get; set; }
    public decimal AlreadyRefunded { get; set; }
    public decimal RefundableAmount { get; set; }
    public string Currency { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime? RefundDeadline { get; set; }
    public bool IsWithinDeadline { get; set; }
    public List<string> EligibilityReasons { get; set; } = new();
    public List<string> IneligibilityReasons { get; set; } = new();
    public Dictionary<string, object>? Constraints { get; set; }
}