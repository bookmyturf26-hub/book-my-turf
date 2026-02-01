namespace BookMyTurfwebservices.Models.DTOs.Responses;

public class RefundResponse
{
    public bool Success { get; set; }
    public string RefundId { get; set; }
    public string PaymentId { get; set; }
    public string OrderId { get; set; }
    public decimal Amount { get; set; }
    public decimal RefundAmount { get; set; }
    public string Currency { get; set; } = "INR";
    public string Status { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public string? GatewayRefundId { get; set; }
    public string? GatewayResponse { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public DateTime? EstimatedSettlementAt { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorCode { get; set; }

    public static RefundResponse SuccessResponse(
        string refundId,
        string paymentId,
        string orderId,
        decimal amount,
        decimal refundAmount,
        string status,
        string gatewayRefundId,
        string? reason = null)
    {
        return new RefundResponse
        {
            Success = true,
            RefundId = refundId,
            PaymentId = paymentId,
            OrderId = orderId,
            Amount = amount,
            RefundAmount = refundAmount,
            Status = status,
            Reason = reason,
            GatewayRefundId = gatewayRefundId,
            RequestedAt = DateTime.UtcNow,
            ProcessedAt = DateTime.UtcNow,
            EstimatedSettlementAt = DateTime.UtcNow.AddDays(5)
        };
    }

    public static RefundResponse FailureResponse(
        string paymentId,
        string errorMessage,
        string? errorCode = null,
        string? reason = null)
    {
        return new RefundResponse
        {
            Success = false,
            PaymentId = paymentId,
            Status = "failed",
            ErrorMessage = errorMessage,
            ErrorCode = errorCode,
            Reason = reason,
            RequestedAt = DateTime.UtcNow
        };
    }
}

public class RefundDetailsResponse
{
    public string RefundId { get; set; }
    public string PaymentId { get; set; }
    public string OrderId { get; set; }
    public string BookingId { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public decimal Amount { get; set; }
    public decimal RefundAmount { get; set; }
    public decimal RefundedAmount { get; set; }
    public decimal RefundableAmount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public string? Notes { get; set; }
    public string GatewayRefundId { get; set; }
    public string PaymentMethod { get; set; }
    public string GatewayStatus { get; set; }
    public string? GatewayErrorCode { get; set; }
    public string? GatewayErrorMessage { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime RefundRequestedAt { get; set; }
    public DateTime? RefundProcessedAt { get; set; }
    public DateTime? RefundSettledAt { get; set; }
    public DateTime? EstimatedSettlementDate { get; set; }
    public List<RefundTransaction> Transactions { get; set; } = new();
    public DateTime ProcessedAt { get; internal set; }
}

public class RefundTransaction
{
    public string TransactionId { get; set; }
    public string Type { get; set; } // "refund", "partial_refund"
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string GatewayTransactionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? Notes { get; set; }
}

public class RefundSummaryResponse
{
    public string PaymentId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalRefunded { get; set; }
    public decimal RefundableAmount { get; set; }
    public int TotalRefunds { get; set; }
    public int SuccessfulRefunds { get; set; }
    public int FailedRefunds { get; set; }
    public int PendingRefunds { get; set; }
    public List<RefundItem> Refunds { get; set; } = new();
    public DateTime? LastRefundDate { get; set; }
}

public class RefundItem
{
    public string RefundId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}

public class BulkRefundResponse
{
    public int TotalRequests { get; set; }
    public int SuccessfulRefunds { get; set; }
    public int FailedRefunds { get; set; }
    public decimal TotalRefundAmount { get; set; }
    public List<BulkRefundItem> Results { get; set; } = new();
    public DateTime ProcessedAt { get; set; }
}

public class BulkRefundItem
{
    public string PaymentId { get; set; }
    public string RefundId { get; set; }
    public bool Success { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string? ErrorMessage { get; set; }
    public string? GatewayRefundId { get; set; }
}