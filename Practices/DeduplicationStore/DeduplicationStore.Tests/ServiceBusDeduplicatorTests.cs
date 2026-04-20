using Azure.Messaging.ServiceBus;
using DeduplicationStore.Strategies;
using FluentAssertions;
using Moq;
using Xunit;

namespace DeduplicationStore.Tests;

public class ServiceBusDeduplicatorTests
{
    private readonly Mock<ServiceBusSender> _senderMock = new();
    private readonly Mock<ServiceBusClient> _clientMock = new();
    private readonly ServiceBusDeduplicator _dedup;

    public ServiceBusDeduplicatorTests()
    {
        _dedup = new ServiceBusDeduplicator(_senderMock.Object, _clientMock.Object);
    }

    [Fact]
    public async Task IsDuplicateAsync_AlwaysReturnsFalse()
    {
        (await _dedup.IsDuplicateAsync("msg-1")).Should().BeFalse();
    }

    [Fact]
    public async Task MarkProcessedAsync_SendsMessageWithCorrectMessageId()
    {
        ServiceBusMessage? captured = null;
        _senderMock
            .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
            .Callback<ServiceBusMessage, CancellationToken>((msg, _) => captured = msg)
            .Returns(Task.CompletedTask);

        await _dedup.MarkProcessedAsync("msg-42");

        captured.Should().NotBeNull();
        captured!.MessageId.Should().Be("msg-42");
    }

    [Fact]
    public async Task MarkProcessedAsync_CallsSendExactlyOnce()
    {
        _senderMock
            .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _dedup.MarkProcessedAsync("msg-1");

        _senderMock.Verify(
            s => s.SendMessageAsync(It.Is<ServiceBusMessage>(m => m.MessageId == "msg-1"), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
