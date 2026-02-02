using BookMyTurfwebservices.Data;
using BookMyTurfwebservices.Models.Entities;
using BookMyTurfwebservices.Models.Enums;
using BookMyTurfwebservices.Services;
using BookMyTurfwebservices.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BookMyTurfwebservices.BackgroundJobs;

[DisallowConcurrentExecution]
public class PaymentStatusCheckerJob : IJob
{
    private readonly ILogger<PaymentStatusCheckerJob> _logger;
    private readonly IServiceProvider _serviceProvider;

    public PaymentStatusCheckerJob(
        ILogger<PaymentStatusCheckerJob> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Payment status checker job started at {Timestamp}",
            DateTime.UtcNow);

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            // Find pending payments older than 30 minutes
            var cutoffTime = DateTime.UtcNow.AddMinutes(-30);

            var pendingPayments = await dbContext.Payments
                .Where(p => p.Status == PaymentStatus.Pending &&
                           p.CreatedAt < cutoffTime)
                .ToListAsync();

            _logger.LogInformation("Found {Count} pending payments to check",
                pendingPayments.Count);

            foreach (var payment in pendingPayments)
            {
                await CheckAndUpdatePaymentStatusAsync(payment, scope);
            }

            _logger.LogInformation("Payment status checker job completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in payment status checker job");
            throw new JobExecutionException(ex, false);
        }
    }

    private async Task CheckAndUpdatePaymentStatusAsync(
        Payment payment,
        IServiceScope scope)
    {
        try
        {
            var razorPayService = scope.ServiceProvider.GetRequiredService<IRazorPayService>();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // In a real implementation, you would call Razorpay API to check payment status
            // For now, we'll mark old pending payments as expired

            if (payment.CreatedAt < DateTime.UtcNow.AddHours(-24))
            {
                payment.Status = PaymentStatus.Expired;
                payment.GatewayResponse = "Payment expired - no action taken within 24 hours";

                await dbContext.SaveChangesAsync();

                _logger.LogInformation("Marked payment {PaymentId} as EXPIRED", payment.Id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to check status for payment {PaymentId}", payment.Id);
        }
    }
}