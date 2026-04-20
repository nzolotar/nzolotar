using DeduplicationStore.Abstractions;
using DeduplicationStore.Data;
using Microsoft.EntityFrameworkCore;

namespace DeduplicationStore.Strategies;

/// <summary>
/// Strategy 2 — Database deduplication using EF Core + SQLite.
///
/// The unique index on ProcessedMessage.MessageId is the real guarantee.
/// MarkProcessedAsync catches the DbUpdateException on constraint violation
/// so the caller never needs to handle it — idempotency is transparent.
///
/// Trade-offs:
///   + Survives restarts, works across instances sharing the same DB.
///   + Durable and auditable (ProcessedAt timestamp).
///   - Requires a database; adds I/O latency on every check.
///   - Needs periodic cleanup to avoid unbounded table growth.
/// </summary>
public sealed class DatabaseDeduplicator : IDeduplicator
{
    private readonly DeduplicationDbContext _db;

    public DatabaseDeduplicator(DeduplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> IsDuplicateAsync(string messageId, CancellationToken ct = default)
    {
        return await _db.ProcessedMessages
            .AnyAsync(m => m.MessageId == messageId, ct);
    }

    public async Task MarkProcessedAsync(string messageId, CancellationToken ct = default)
    {
        _db.ProcessedMessages.Add(new ProcessedMessage
        {
            MessageId = messageId,
            ProcessedAt = DateTimeOffset.UtcNow
        });

        try
        {
            await _db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
        {
            // A concurrent worker already inserted this messageId.
            // Detach the failed entity so the context is reusable.
            _db.ChangeTracker.Clear();
        }
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        // SQLite error code 19 = SQLITE_CONSTRAINT; message contains "UNIQUE"
        return ex.InnerException?.Message.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase) == true;
    }
}
