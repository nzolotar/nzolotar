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
        """;

    public void Run()
    {
        foreach (var s in (string[])["leetcode", "loveleet", "aabb", "z"])
        {
            int i = Solve(s);
            Console.WriteLine($"  \"{s}\" → {(i >= 0 ? $"{i} ('{s[i]}')" : "-1")}");
        }
    }

    private static int Solve(string s)
    {
        var freq = new Dictionary<char, int>();
        foreach (char c in s)
            freq[c] = freq.GetValueOrDefault(c) + 1;

        for (int i = 0; i < s.Length; i++)
            if (freq[s[i]] == 1)
                return i;

        return -1;
    }
}
