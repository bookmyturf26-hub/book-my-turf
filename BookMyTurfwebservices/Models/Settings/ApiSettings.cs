namespace BookMyTurfwebservices.Models.Settings;

public class ApiSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    public int RateLimitRequests { get; set; } = 100;
    public int RateLimitPeriodMinutes { get; set; } = 1;
}