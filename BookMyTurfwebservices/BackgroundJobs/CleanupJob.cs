using BookMyTurfwebservices.Data;
using BookMyTurfwebservices.Models.Enums;
using BookMyTurfwebservices.Utilities;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BookMyTurfwebservices.BackgroundJobs;

[DisallowConcurrentExecution]
public class CleanupJob : IJob
{
    private readonly ILogger<CleanupJob> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CleanupJob(
        ILogger<CleanupJob> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Cleanup job started at {Timestamp}", DateTime.UtcNow);

        using var scope = _serviceProvider.CreateScope();

        try
        {
            // Cleanup old idempotency keys
            var idempotencyChecker = scope.ServiceProvider.GetRequiredService<IIdempotencyChecker>();
            await idempotencyChecker.CleanupOldIdempotencyKeysAsync();

            // Add other cleanup tasks here
            await CleanupOldWebhookLogsAsync(scope);
            await CleanupOldPaymentRecordsAsync(scope);

            _logger.LogInformation("Cleanup job completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in cleanup job");
            throw new JobExecutionException(ex, false);
        }
    }

    private async Task CleanupOldWebhookLogsAsync(IServiceScope scope)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var cutoffTime = DateTime.UtcNow.AddMonths(-6); // Keep logs for 6 months

        var oldLogs = await dbContext.WebhookLogs
            .Where(w => w.ReceivedAt < cutoffTime)
            .ToListAsync();

        if (oldLogs.Any())
        {
            dbContext.WebhookLogs.RemoveRange(oldLogs);
            await dbContext.SaveChangesAsync();

            _logger.LogInformation("Cleaned up {Count} old webhook logs", oldLogs.Count);
        }
    }

    private async Task CleanupOldPaymentRecordsAsync(IServiceScope scope)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var cutoffTime = DateTime.UtcNow.AddYears(-1); // Keep records for 1 year

        // Mark old failed payments for archival
        var oldFailedPayments = await dbContext.Payments
            .Where(p => p.Status == PaymentStatus.Failed &&
                       p.CreatedAt < cutoffTime)
            .ToListAsync();

        // In production, you might want to archive these instead of deleting
        foreach (var payment in oldFailedPayments)
        {
            payment.Notes += " [ARCHIVED]";
        }

        await dbContext.SaveChangesAsync();

        _logger.LogInformation("Archived {Count} old failed payments", oldFailedPayments.Count);
    }
}