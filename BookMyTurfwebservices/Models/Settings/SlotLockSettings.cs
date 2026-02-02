namespace BookMyTurfwebservices.Models.Settings;

public class SlotLockSettings
{
    public int LockDurationMinutes { get; set; } = 10;
    public int CleanupIntervalMinutes { get; set; } = 5;
    public int MaxLockExtensions { get; set; } = 2;
}