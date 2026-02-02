namespace BookMyTurfwebservices.Models.Settings;

public class PaymentGatewaySettings
{
    public string Provider { get; set; } = string.Empty;
    public RazorpaySettings Razorpay { get; set; } = new RazorpaySettings();
    public int TimeoutInSeconds { get; set; } = 30;
    public int RetryCount { get; set; } = 3;
}

public class RazorpaySettings
{
    public string KeyId { get; set; } = string.Empty;
    public string KeySecret { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;
}