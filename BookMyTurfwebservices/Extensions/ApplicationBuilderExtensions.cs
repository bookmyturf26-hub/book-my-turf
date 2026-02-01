using BookMyTurfwebservices.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyTurfwebservices.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task EnsureDatabaseCreatedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();

        // Remove SeedData.InitializeAsync call since it doesn't exist
        // You can add it back after creating SeedData class
    }
}