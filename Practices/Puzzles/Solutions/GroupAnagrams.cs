namespace Puzzles.Solutions;

// LC #49 — Group Anagrams
// Group strings that are anagrams of each other.
// Approach: sort each word's chars as a key, group by it.
// O(n * k log k) where k is max word length.
public class GroupAnagrams : IPuzzle
{
    public string Name => "Group Anagrams";
    public string Description => "LC #49 — group strings that are anagrams of each other";
    public string Explanation =>
        """
        Two words are anagrams if they contain the same letters — so sorting their characters
        produces the same string. Use that sorted version as a grouping key.
        Words that map to the same key are anagrams of each other.

        Performance:
          Time:  O(n · k log k) — each of the n words is sorted by its k characters.
          Space: O(n · k) — storing all words and their sorted keys.
        """;

    public void Run()
    {
        Print(Solve(["eat", "tea", "tan", "ate", "nat", "bat"]));
        Print(Solve([""]));
        Print(Solve(["a"]));
    }

    private static IEnumerable<IGrouping<string, string>> Solve(string[] strs) =>
        strs.GroupBy(s => new string(s.Order().ToArray()));

    private static void Print(IEnumerable<IGrouping<string, string>> groups)
    {
        foreach (var g in groups)
            Console.WriteLine($"[{string.Join(", ", g)}]");
        Console.WriteLine();
    }
}
