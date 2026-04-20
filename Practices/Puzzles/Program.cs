using Puzzles;
using Puzzles.Solutions;

// ── Register puzzles here ──────────────────────────────────────────────────
List<IPuzzle> puzzles =
[
    new TwoSum(),
    new GroupAnagrams(),
    new TopKFrequent(),
    new PairSum(),
    new ThreeOfAKind(),
    new FizzBuzz(),
    new ReverseString(),
    new Palindrome(),
    new FirstUniqueChar(),
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
        var puzzle = puzzles[choice - 1];
        Console.Clear();
        Console.WriteLine($"── {puzzle.Name} ──");
        Console.WriteLine();
        Console.WriteLine(puzzle.Explanation);
        Console.WriteLine();
        puzzle.Run();
    }
    else
    {
        Console.WriteLine("Invalid choice.");
    }

    Console.WriteLine();
    Console.Write("Press any key to return to menu...");
    Console.ReadKey();
}
