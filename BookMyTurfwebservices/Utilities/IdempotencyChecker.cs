using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using BookMyTurfwebservices.Data;
using BookMyTurfwebservices.Models.Entities;

namespace BookMyTurfwebservices.Utilities;

public interface IIdempotencyChecker
{
    Task<bool> IsDuplicateRequestAsync(string idempotencyKey);
    Task MarkRequestAsProcessedAsync(string idempotencyKey);
    Task CleanupOldIdempotencyKeysAsync();
}

public class IdempotencyChecker : IIdempotencyChecker
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<IdempotencyChecker> _logger;
    private const int CacheExpirationMinutes = 60;

    public IdempotencyChecker(
        ApplicationDbContext context,
        IMemoryCache memoryCache,
        ILogger<IdempotencyChecker> logger)
    {
        _context = context;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<bool> IsDuplicateRequestAsync(string idempotencyKey)
    {
        // Check memory cache first
        if (_memoryCache.TryGetValue(idempotencyKey, out _))
        {
            _logger.LogDebug("Duplicate request detected in cache: {Key}", idempotencyKey);
            return true;
        }

        // Check database
        var existingKey = await _context.IdempotencyKeys
            .FirstOrDefaultAsync(k => k.Key == idempotencyKey &&
                                     k.ExpiresAt > DateTime.UtcNow);

        if (existingKey != null)
        {
            // Cache the result for faster subsequent checks
            _memoryCache.Set(idempotencyKey, true, TimeSpan.FromMinutes(CacheExpirationMinutes));

            _logger.LogDebug("Duplicate request detected in database: {Key}", idempotencyKey);
            return true;
        }

        return false;
    }

    public async Task MarkRequestAsProcessedAsync(string idempotencyKey)
    {
        try
        {
            var idempotencyRecord = new IdempotencyKey
            {
                Id = Guid.NewGuid().ToString(),
                Key = idempotencyKey,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };

            await _context.IdempotencyKeys.AddAsync(idempotencyRecord);
            await _context.SaveChangesAsync();

            // Cache the key
            _memoryCache.Set(idempotencyKey, true, TimeSpan.FromMinutes(CacheExpirationMinutes));

            _logger.LogDebug("Marked request as processed: {Key}", idempotencyKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to mark request as processed: {Key}", idempotencyKey);
            throw;
        }
    }

    public async Task CleanupOldIdempotencyKeysAsync()
    {
        try
        {
            var cutoffTime = DateTime.UtcNow.AddHours(-24);

            var oldKeys = await _context.IdempotencyKeys
                .Where(k => k.ExpiresAt < cutoffTime)
                .ToListAsync();

            if (oldKeys.Any())
            {
                _context.IdempotencyKeys.RemoveRange(oldKeys);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cleaned up {Count} old idempotency keys", oldKeys.Count);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to cleanup old idempotency keys");
            throw;
        }
    }
}

// Add this entity to your Models/Entities folder
public class IdempotencyKey
{
    public string Id { get; set; }
    public string Key { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}