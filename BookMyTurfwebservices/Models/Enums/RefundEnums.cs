namespace BookMyTurfwebservices.Models.Enums;

public enum RefundStatus
{
    Pending = 1,
    Processing = 2,
    Processed = 3,
    Failed = 4,
    Cancelled = 5,
    Reversed = 6
}

public enum RefundType
{
    Full = 1,
    Partial = 2
}

public enum RefundSpeed
{
    Normal = 1,
    Instant = 2
}

public enum RefundInitiatedBy
{
    Customer = 1,
    Merchant = 2,
    System = 3
}

public enum RefundReasonCode
{
    CustomerCancellation = 100,
    MerchantCancellation = 101,
    TurfUnavailable = 102,
    BadWeather = 103,
    DuplicatePayment = 200,
    PaymentError = 201,
    TechnicalError = 300,
    ServiceNotAsDescribed = 400,
    CustomerDissatisfaction = 401,
    Other = 999
}