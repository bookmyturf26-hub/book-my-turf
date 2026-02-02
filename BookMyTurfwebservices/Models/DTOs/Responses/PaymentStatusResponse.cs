namespace BookMyTurfwebservices.Models.DTOs.Responses;

public class PaymentStatusResponse
{
    public string PaymentId { get; set; }
    public string OrderId { get; set; }
    public string? TransactionId { get; set; }
    public string BookingId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "INR";
    public string Status { get; set; }
    public string? PaymentMethod { get; set; }
    public string? GatewayStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime? RefundedAt { get; set; }
    public decimal? RefundAmount { get; set; }
    public string? RefundReason { get; set; }
    public TransactionInfo? LatestTransaction { get; set; }
    public List<TransactionInfo> Transactions { get; set; } = new();
    public bool IsRefundable { get; set; }
    public decimal RefundableAmount { get; set; }
    public DateTime? RefundDeadline { get; set; }
}

public class TransactionInfo
{
    public string TransactionId { get; set; }
    public string Type { get; set; } // "payment", "refund", "partial_refund"
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string? GatewayTransactionId { get; set; }
    public string? GatewayResponse { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? Notes { get; set; }
}