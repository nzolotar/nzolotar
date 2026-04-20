namespace DeduplicationStore.Data;

/// <summary>
/// EF Core entity representing a message that has been processed.
/// The unique index on MessageId is the database-level deduplication guard.
/// </summary>
public sealed class ProcessedMessage
{
    public int Id { get; set; }

    /// <summary>
    /// The application-level message identifier. Must be unique.
    /// </summary>
    public string MessageId { get; set; } = string.Empty;

    public DateTimeOffset ProcessedAt { get; set; } = DateTimeOffset.UtcNow;
}
