using Microsoft.EntityFrameworkCore;

namespace BookMyTurfwebservices.Data;

public class ApplicationDbContextReadOnly : ApplicationDbContext
{
    public ApplicationDbContextReadOnly(DbContextOptions<ApplicationDbContext> options) // Fixed constructor
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}