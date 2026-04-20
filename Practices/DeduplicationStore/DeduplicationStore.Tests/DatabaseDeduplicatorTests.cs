using DeduplicationStore.Data;
using DeduplicationStore.Strategies;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DeduplicationStore.Tests;

public class DatabaseDeduplicatorTests : IAsyncLifetime
{
    private SqliteConnection _connection = null!;
    private DeduplicationDbContext _db = null!;
    private DatabaseDeduplicator _dedup = null!;

    public async Task InitializeAsync()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<DeduplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        _db = new DeduplicationDbContext(options);
        await _db.Database.EnsureCreatedAsync();
        _dedup = new DatabaseDeduplicator(_db);
    }

    public async Task DisposeAsync()
    {
        await _db.DisposeAsync();
        await _connection.DisposeAsync();
    }

    [Fact]
    public async Task IsDuplicateAsync_ReturnsFalse_ForNewMessage()
    {
        (await _dedup.IsDuplicateAsync("order-1")).Should().BeFalse();
    }

    [Fact]
    public async Task IsDuplicateAsync_ReturnsTrue_AfterMarkProcessed()
    {
        await _dedup.MarkProcessedAsync("order-1");
        (await _dedup.IsDuplicateAsync("order-1")).Should().BeTrue();
    }

    [Fact]
    public async Task MarkProcessedAsync_PersistsRow_ToDatabase()
    {
        await _dedup.MarkProcessedAsync("order-1");
        (await _db.ProcessedMessages.CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task MarkProcessedAsync_SetsProcessedAt_Timestamp()
    {
        var before = DateTimeOffset.UtcNow;
        await _dedup.MarkProcessedAsync("order-1");
        var row = await _db.ProcessedMessages.SingleAsync(m => m.MessageId == "order-1");
        row.ProcessedAt.Should().BeOnOrAfter(before);
    }

    [Fact]
    public async Task MarkProcessedAsync_DoesNotThrow_WhenConcurrentInsertViolatesConstraint()
    {
        // Simulate another worker inserting first (bypass the deduplicator)
        _db.ProcessedMessages.Add(new ProcessedMessage { MessageId = "order-1", ProcessedAt = DateTimeOffset.UtcNow });
        await _db.SaveChangesAsync();
        _db.ChangeTracker.Clear();

        var act = async () => await _dedup.MarkProcessedAsync("order-1");
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task MarkProcessedAsync_DoesNotInsertDuplicate_WhenConstraintViolationOccurs()
    {
        _db.ProcessedMessages.Add(new ProcessedMessage { MessageId = "order-1", ProcessedAt = DateTimeOffset.UtcNow });
        await _db.SaveChangesAsync();
        _db.ChangeTracker.Clear();

        await _dedup.MarkProcessedAsync("order-1");

        (await _db.ProcessedMessages.CountAsync(m => m.MessageId == "order-1")).Should().Be(1);
    }
}
