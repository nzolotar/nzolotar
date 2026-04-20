using Puzzles;
using Puzzles.Solutions;

// ── Register puzzles here ──────────────────────────────────────────────────
List<IPuzzle> puzzles =
[
    new TwoSum(),
    new GroupAnagrams(),
    new TopKFrequent(),
    new PairSum(),
];
// ──────────────────────────────────────────────────────────────────────────

while (true)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════╗");
    Console.WriteLine("║          Puzzles             ║");
    Console.WriteLine("╚══════════════════════════════╝");
    Console.WriteLine();

    for (int i = 0; i < puzzles.Count; i++)
        Console.WriteLine($"  {i + 1}. {puzzles[i].Name}  —  {puzzles[i].Description}");

    Console.WriteLine();
    Console.WriteLine("  0. Exit");
    Console.WriteLine();
    Console.Write("Pick a puzzle: ");

    string? input = Console.ReadLine();

    if (input == "0") break;

    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= puzzles.Count)
    {
        Console.Clear();
        Console.WriteLine($"── {puzzles[choice - 1].Name} ──");
        Console.WriteLine();
        puzzles[choice - 1].Run();
    }
    else
    {
        Console.WriteLine("Invalid choice.");
    }

    Console.WriteLine();
    Console.Write("Press any key to return to menu...");
    Console.ReadKey();
}
