using DeduplicationStore.Strategies;
using Microsoft.Extensions.Caching.Memory;

namespace DeduplicationStore.Demos;

public static class InMemoryDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== Strategy 1: In-Memory (IMemoryCache + TTL) ===");
        Console.WriteLine();

        var cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 });
        var deduplicator = new InMemoryDeduplicator(cache, ttl: TimeSpan.FromSeconds(10));

        var messages = new[]
        {
            "msg-001", "msg-002", "msg-001", // msg-001 is a duplicate
            "msg-003", "msg-002"             // msg-002 is a duplicate
        };

        foreach (var id in messages)
        {
            bool isDuplicate = await deduplicator.IsDuplicateAsync(id);

            if (isDuplicate)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  [DUPLICATE SKIPPED] {id}");
            }
            else
            {
                await deduplicator.MarkProcessedAsync(id);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  [PROCESSED]         {id}");
            }
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine("  Waiting 12 seconds for TTL to expire...");
        await Task.Delay(TimeSpan.FromSeconds(12));

        bool expiredCheck = await deduplicator.IsDuplicateAsync("msg-001");
        Console.WriteLine($"  msg-001 is duplicate after TTL expiry: {expiredCheck} (expected: False)");
        Console.WriteLine();
    }
}
