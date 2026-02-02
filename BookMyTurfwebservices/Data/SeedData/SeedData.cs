using BookMyTurfwebservices.Data;
using BookMyTurfwebservices.Models.Entities;
using BookMyTurfwebservices.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookMyTurfwebservices.Data.SeedData;

public static class SeedData
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        // Apply any pending migrations
        await context.Database.MigrateAsync();

        // Check if we already have data
        if (!await context.Payments.AnyAsync())
        {
            // Seed initial data here if needed
            var testPayment = new Payment
            {
                Id = Guid.NewGuid().ToString(),
                BookingId = "TEST-BOOKING-001",
                OrderId = "order_" + Guid.NewGuid().ToString().Substring(0, 8),
                Amount = 1000.00m,
                Status = PaymentStatus.Completed,
                PaymentMethod = "UPI",
                Currency = "INR",
                CustomerEmail = "test@example.com",
                CustomerPhone = "9876543210",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                PaidAt = DateTime.UtcNow.AddDays(-1)
            };

            await context.Payments.AddAsync(testPayment);
            await context.SaveChangesAsync();
        }
    }
}