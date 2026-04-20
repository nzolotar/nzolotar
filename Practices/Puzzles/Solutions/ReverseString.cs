namespace Puzzles.Solutions;

// Reverse a string without Reverse().
// Approach: iterate from the last character to the first, appending each to a StringBuilder.
// O(n) time, O(n) space.
public class ReverseString : IPuzzle
{
    public string Name => "Reverse String";
    public string Description => "Reverse a string without using Reverse()";
    public string Explanation =>
        """
        Walk the string from the last index down to 0, appending each character to a StringBuilder.
        Reading backwards and writing forwards produces the reversed string.

        Why StringBuilder over string concatenation?
        Strings in C# are immutable — result += s[i] allocates a brand new string on every iteration,
        copying all previously accumulated characters each time. That's O(1 + 2 + ... + n) = O(n²) allocations.
        StringBuilder maintains a resizable internal buffer and only allocates when that buffer is full,
        making the whole loop O(n). For short strings the difference is negligible;
        for long ones it's the difference between fast and unusably slow.

        Performance:
          Time:  O(n) — one pass through the string.
          Space: O(n) — the StringBuilder holds a full copy of the input.
        """;

    public void Run()
    {
        string[] inputs = ["hello", "abcde", "a", ""];

        Console.WriteLine("StringBuilder:");
        foreach (var s in inputs)
            Console.WriteLine($"  \"{s}\" → \"{Solve(s)}\"");

        Console.WriteLine("\nLINQ (note: uses Enumerable.Reverse, not string.Reverse):");
        foreach (var s in inputs)
            Console.WriteLine($"  \"{s}\" → \"{SolveLinq(s)}\"");
    }

    private static string Solve(string s)
    {
        var result = new System.Text.StringBuilder();
        for (int i = s.Length - 1; i >= 0; i--)
            result.Append(s[i]);
        return result.ToString();
    }

    // Concise — Enumerable.Reverse iterates chars backwards, new string() reassembles them
    private static string SolveLinq(string s) => new string(s.Reverse().ToArray());
}
