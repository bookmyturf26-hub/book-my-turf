using BookMyTurfwebservices.Models.Entities;
using BookMyTurfwebservices.Models.Interfaces;
using BookMyTurfwebservices.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;

namespace BookMyTurfwebservices.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();
    public DbSet<WebhookLog> WebhookLogs => Set<WebhookLog>();
    public DbSet<IdempotencyKey> IdempotencyKeys => Set<IdempotencyKey>();
    public DbSet<RefundRequestEntity> RefundRequests => Set<RefundRequestEntity>();
    public DbSet<BookingSyncLog> BookingSyncLogs => Set<BookingSyncLog>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure decimal precision for all entities
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                {
                    property.SetPrecision(18);
                    property.SetScale(2);
                }
            }
        }

        // Configure soft delete filter - FIXED
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                // Create the filter expression
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, "IsDeleted");
                var equals = Expression.Equal(property, Expression.Constant(false));
                var lambda = Expression.Lambda(equals, parameter);

                // Apply the query filter
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        // Alternative simpler approach:
        ConfigureSoftDeleteFilter<Payment>(modelBuilder);
        ConfigureSoftDeleteFilter<RefundRequestEntity>(modelBuilder);
        // Add other entities that implement ISoftDeletable
    }

    private void ConfigureSoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : class, ISoftDeletable
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update audit fields
        UpdateAuditFields();

        // Handle soft delete
        HandleSoftDelete();

        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        // Update audit fields
        UpdateAuditFields();

        // Handle soft delete
        HandleSoftDelete();

        return base.SaveChanges();
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditable &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        var currentTime = DateTime.UtcNow;
        var currentUser = GetCurrentUser();

        foreach (var entry in entries)
        {
            var entity = (IAuditable)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = currentTime;
                entity.CreatedBy = currentUser ?? "System";
            }

            entity.UpdatedAt = currentTime;
            entity.UpdatedBy = currentUser ?? "System";
        }
    }

    private void HandleSoftDelete()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is ISoftDeletable &&
                       e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            var entity = (ISoftDeletable)entry.Entity;
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            entity.DeletedBy = GetCurrentUser() ?? "System";
        }
    }

    private string? GetCurrentUser()
    {
        // Implement based on your authentication system
        return "System";
    }

    // Transaction helper methods
    public async Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default)
    {
        return await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
    }

    // Check if entity exists
    public async Task<bool> ExistsAsync<T>(string id) where T : class
    {
        return await Set<T>().AnyAsync(e => EF.Property<string>(e, "Id") == id);
    }

    // Find with includes
    public async Task<T?> FindWithIncludesAsync<T>(string id, params string[] includes) where T : class
    {
        var query = Set<T>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Id") == id);
    }
}