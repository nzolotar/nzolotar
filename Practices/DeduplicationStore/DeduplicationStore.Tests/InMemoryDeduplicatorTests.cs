using DeduplicationStore.Strategies;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace DeduplicationStore.Tests;

public class InMemoryDeduplicatorTests
{
    private static InMemoryDeduplicator Create(TimeSpan? ttl = null)
    {
        var cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 });
        return new InMemoryDeduplicator(cache, ttl);
    }

    [Fact]
    public async Task IsDuplicateAsync_ReturnsFalse_ForNewMessage()
    {
        var dedup = Create();
        (await dedup.IsDuplicateAsync("msg-1")).Should().BeFalse();
    }

    [Fact]
    public async Task IsDuplicateAsync_ReturnsTrue_AfterMarkProcessed()
    {
        var dedup = Create();
        await dedup.MarkProcessedAsync("msg-1");
        (await dedup.IsDuplicateAsync("msg-1")).Should().BeTrue();
    }

    [Fact]
    public async Task IsDuplicateAsync_ReturnsFalse_ForUnseenMessageWhenOthersExist()
    {
        var dedup = Create();
        await dedup.MarkProcessedAsync("msg-1");
        (await dedup.IsDuplicateAsync("msg-2")).Should().BeFalse();
    }

    [Fact]
    public async Task IsDuplicateAsync_ReturnsFalse_AfterTtlExpiry()
    {
        var dedup = Create(ttl: TimeSpan.FromMilliseconds(100));
        await dedup.MarkProcessedAsync("msg-1");
        await Task.Delay(250);
        (await dedup.IsDuplicateAsync("msg-1")).Should().BeFalse();
    }

    [Fact]
    public async Task MarkProcessedAsync_IsIdempotent_DoesNotThrow()
    {
        var dedup = Create();
        await dedup.MarkProcessedAsync("msg-1");
        var act = async () => await dedup.MarkProcessedAsync("msg-1");
        await act.Should().NotThrowAsync();
    }
}
