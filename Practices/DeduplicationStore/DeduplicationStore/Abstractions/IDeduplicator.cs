namespace DeduplicationStore.Abstractions;

/// <summary>
/// Shared contract for all deduplication strategies.
/// </summary>
public interface IDeduplicator
{
    /// <summary>
    /// Returns true if the given messageId has already been processed.
    /// </summary>
    Task<bool> IsDuplicateAsync(string messageId, CancellationToken ct = default);

    /// <summary>
    /// Records the messageId as processed so future calls to IsDuplicateAsync return true.
    /// </summary>
    Task MarkProcessedAsync(string messageId, CancellationToken ct = default);
}
