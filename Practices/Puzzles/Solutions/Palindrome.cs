namespace Puzzles.Solutions;

// Determine if a string is a palindrome, ignoring spaces and casing.
public class Palindrome : IPuzzle
{
    public string Name => "Palindrome";
    public string Description => "Check if a string is a palindrome, ignoring spaces and casing";
    public string Explanation =>
        """
        Option 1 — Reverse and compare:
        Normalize (lowercase, strip spaces), reverse into a StringBuilder, compare to the original.
        Simple to read.

        Option 2 — Two pointers:
        Normalize first, then walk inward from both ends comparing characters.
        No reversal needed — if every mirrored pair matches, it's a palindrome.

        Performance:
          Option 1 — Time: O(n)  Space: O(n) — reversed string is a full copy of the input.
          Option 2 — Time: O(n)  Space: O(n) — normalized string is still a full copy.

        Note: true O(1) space would require skipping spaces inline without normalizing first,
        so the pointers hop over spaces directly on the original string.
        """;

    public void Run()
    {
        string[] inputs = [
            "racecar",
            "A man a plan a canal Panama",
            "hello",
            "Was it a car or a cat I saw",
            "Not a palindrome",
        ];

        Console.WriteLine("Option 1 — reverse and compare:");
        foreach (var s in inputs)
            Console.WriteLine($"  \"{s}\" → {SolveWithReverse(s)}");

        Console.WriteLine();
        Console.WriteLine("Option 2 — two pointers:");
        foreach (var s in inputs)
            Console.WriteLine($"  \"{s}\" → {SolveWithTwoPointers(s)}");

        Console.WriteLine();
        Console.WriteLine("Option 3 — LINQ SequenceEqual:");
        foreach (var s in inputs)
            Console.WriteLine($"  \"{s}\" → {SolveLinq(s)}");
    }

    // O(n) time, O(n) space
    private static bool SolveWithReverse(string input)
    {
        string cleaned = input.ToLower().Replace(" ", "");

        var reversed = new System.Text.StringBuilder();
        for (int i = cleaned.Length - 1; i >= 0; i--)
            reversed.Append(cleaned[i]);

        return reversed.ToString() == cleaned;
    }

    // Cleanest LINQ version — SequenceEqual compares cleaned string to its reverse
    private static bool SolveLinq(string input)
    {
        string cleaned = input.ToLower().Replace(" ", "");
        return cleaned.SequenceEqual(cleaned.Reverse());
    }

    // O(n) time, O(1) extra space
    private static bool SolveWithTwoPointers(string input)
    {
        string cleaned = input.ToLower().Replace(" ", "");
        int left = 0;
        int right = cleaned.Length - 1;

        while (left < right)
        {
            if (cleaned[left] != cleaned[right])
                return false;
            left++;
            right--;
        }
        return true;
    }
}
