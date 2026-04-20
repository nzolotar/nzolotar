namespace Puzzles.Solutions;

// Given a string, find the first non-repeating character and return its index.
// Return -1 if none exists.
public class FirstUniqueChar : IPuzzle
{
    public string Name => "First Unique Character";
    public string Description => "Find the index of the first non-repeating character";
    public string Explanation =>
        """
        Two passes:
          1. Build a frequency map — count how many times each character appears.
          2. Walk the string again and return the index of the first character with count == 1.

        Why two passes and not one?
        On a single pass you don't yet know if a character repeats later in the string.
        You need the full picture before you can judge uniqueness.

        Performance:
          Time:  O(n) — two linear passes, each O(n).
          Space: O(1) — the map holds at most 128 entries for ASCII,
                        or O(k) for k unique characters in an arbitrary character set.
        """;

    public void Run()
    {
        string[] inputs = ["leetcode", "loveleet", "aabb", "z"];

        Console.WriteLine("Two-pass dictionary:");
        foreach (var s in inputs)
        {
            int i = Solve(s);
            Console.WriteLine($"  \"{s}\" → {(i >= 0 ? $"{i} ('{s[i]}')" : "-1")}");
        }

        Console.WriteLine("\nLINQ:");
        foreach (var s in inputs)
        {
            int i = SolveLinq(s);
            Console.WriteLine($"  \"{s}\" → {(i >= 0 ? $"{i} ('{s[i]}')" : "-1")}");
        }
    }

    private static int Solve(string s)
    {
        Dictionary<char, int> counts = new Dictionary<char, int>();

        // Step 1: Count occurrences
        foreach (char c in s)
        {
            if (counts.ContainsKey(c))
                counts[c]++;
            else
                counts[c] = 1;
        }

        // Step 2: Find the first index with a count of 1
        for (int i = 0; i < s.Length; i++)
        {
            if (counts[s[i]] == 1)
            {
                return i;
            }
        }

        return -1;
    }

    // Find the first char with count 1, then locate its index — O(n) but two scans via LINQ
    private static int SolveLinq(string s)
    {
        var unique = s.GroupBy(c => c).Where(g => g.Count() == 1).Select(g => g.Key).ToHashSet();
        var match = s.Select((c, i) => (c, i)).FirstOrDefault(x => unique.Contains(x.c));
        return match == default ? -1 : match.i;
    }
}
