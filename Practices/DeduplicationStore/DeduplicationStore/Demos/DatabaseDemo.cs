using DeduplicationStore.Data;
using DeduplicationStore.Strategies;
using Microsoft.EntityFrameworkCore;

namespace DeduplicationStore.Demos;

public static class DatabaseDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("=== Strategy 2: Database (EF Core + SQLite unique constraint) ===");
        Console.WriteLine();

        const string dbPath = "dedup_demo.db";

        if (File.Exists(dbPath))
            File.Delete(dbPath);

        var options = new DbContextOptionsBuilder<DeduplicationDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        await using var db = new DeduplicationDbContext(options);
        await db.Database.EnsureCreatedAsync();

        var deduplicator = new DatabaseDeduplicator(db);

        var messages = new[]
        {
            "order-100", "order-101", "order-100", // order-100 is a duplicate
            "order-102", "order-101"               // order-101 is a duplicate
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

        int count = await db.ProcessedMessages.CountAsync();
        Console.WriteLine();
        Console.WriteLine($"  Total rows in ProcessedMessages table: {count} (expected: 3)");
        Console.WriteLine();

        if (File.Exists(dbPath))
            File.Delete(dbPath);
    }
}
