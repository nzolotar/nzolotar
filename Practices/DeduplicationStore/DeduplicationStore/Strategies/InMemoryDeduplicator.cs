using DeduplicationStore.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace DeduplicationStore.Strategies;

/// <summary>
/// Strategy 1 — In-Memory deduplication using IMemoryCache.
///
/// Trade-offs:
///   + Zero infrastructure, perfect for single-instance services or short bursts.
///   - State is lost on process restart.
///   - Does not work across multiple service instances.
///   - TTL means very old duplicates can slip through after expiry.
/// </summary>
public sealed class InMemoryDeduplicator : IDeduplicator
{
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _ttl;

    // Sentinel object — we only care about key presence, not value.
    private static readonly object Marker = new();

    public InMemoryDeduplicator(IMemoryCache cache, TimeSpan? ttl = null)
    {
        _cache = cache;
        _ttl = ttl ?? TimeSpan.FromMinutes(5);
    }

    public Task<bool> IsDuplicateAsync(string messageId, CancellationToken ct = default)
    {
        return Task.FromResult(_cache.TryGetValue(CacheKey(messageId), out _));
    }

    public Task MarkProcessedAsync(string messageId, CancellationToken ct = default)
    {
        _cache.Set(CacheKey(messageId), Marker, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _ttl,
            Size = 1
        });
        return Task.CompletedTask;
    }

    private static string CacheKey(string messageId) => $"dedup:{messageId}";
}
