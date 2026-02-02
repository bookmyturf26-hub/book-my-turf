namespace BookMyTurfwebservices.Models.Enums;

public enum PaymentStatus
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Refunded = 4,
    PartiallyRefunded = 5, // Add this
    Expired = 6,
    Cancelled = 7
}

public enum PaymentMethod
{
    UPI = 1,
    Card = 2,
    NetBanking = 3,
    Wallet = 4
}

public enum TransactionType
{
    OrderCreation = 1,
    Payment = 2,
    FullRefund = 3, // Add this
    PartialRefund = 4 // Add this
}