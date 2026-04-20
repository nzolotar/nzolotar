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
        Simple to read. O(n) time, O(n) space — the reversed string is a full copy.

        Option 2 — Two pointers:
        Skip normalization into a new string. Walk inward from both ends of the original,
        skipping spaces and comparing characters case-insensitively in place.
        O(n) time, O(1) extra space — nothing is allocated.
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
        Console.WriteLine("Option 2 — two pointers, O(1) space:");
        foreach (var s in inputs)
            Console.WriteLine($"  \"{s}\" → {SolveWithTwoPointers(s)}");
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
