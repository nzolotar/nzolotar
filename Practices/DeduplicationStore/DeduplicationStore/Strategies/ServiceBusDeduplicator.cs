using Azure.Messaging.ServiceBus;
using DeduplicationStore.Abstractions;

namespace DeduplicationStore.Strategies;

/// <summary>
/// Strategy 3 — Azure Service Bus built-in duplicate detection.
///
/// HOW IT WORKS:
///   Service Bus queues/topics support a "duplicate detection history time window"
///   (configured on the queue, typically 10 minutes to 7 days).
///   When you set ServiceBusMessage.MessageId before sending, the broker tracks
///   that ID in a ring buffer for the configured window. If the same MessageId
///   arrives again within the window, the broker silently discards it — no consumer
///   ever sees the duplicate. No application-side state is required.
///
/// This class is a thin wrapper demonstrating the required send pattern.
/// IsDuplicateAsync is intentionally not meaningful here — the broker owns
/// the deduplication state. It is included only to satisfy IDeduplicator
/// for demonstration parity.
///
/// Trade-offs:
///   + No application state; deduplication is offloaded to the broker.
///   + Works transparently across all consumers and service instances.
///   - Requires Azure Service Bus (Standard tier or above).
///   - Window-bounded: duplicates outside the window are not detected.
///   - MessageId must be set by the sender — consumers cannot retroactively dedup.
///
/// Required queue settings:
///   requiresDuplicateDetection: true
///   duplicateDetectionHistoryTimeWindow: PT10M  (ISO 8601, max 7 days)
/// </summary>
public sealed class ServiceBusDeduplicator : IDeduplicator, IAsyncDisposable
{
    private readonly ServiceBusSender _sender;
    private readonly ServiceBusClient _client;

    public ServiceBusDeduplicator(string connectionString, string queueName)
    {
        _client = new ServiceBusClient(connectionString);
        _sender = _client.CreateSender(queueName);
    }

    // Internal constructor for testing — accepts a pre-built sender.
    internal ServiceBusDeduplicator(ServiceBusSender sender, ServiceBusClient client)
    {
        _sender = sender;
        _client = client;
    }

    /// <summary>
    /// Not applicable for the Service Bus strategy — the broker handles deduplication.
    /// Always returns false; checking for duplicates is the broker's responsibility.
    /// </summary>
    public Task<bool> IsDuplicateAsync(string messageId, CancellationToken ct = default)
        => Task.FromResult(false);

    /// <summary>
    /// Sends a message with the MessageId set. The broker will deduplicate
    /// any subsequent send of the same MessageId within the detection window.
    /// </summary>
    public async Task MarkProcessedAsync(string messageId, CancellationToken ct = default)
    {
        var message = new ServiceBusMessage($"Payload for {messageId}")
        {
            // This is the key line: setting MessageId enables broker-side deduplication.
            MessageId = messageId
        };

        await _sender.SendMessageAsync(message, ct);
    }

    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
        await _client.DisposeAsync();
    }
}
