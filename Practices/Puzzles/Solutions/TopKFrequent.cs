namespace Puzzles.Solutions;

// LC #347 — Top K Frequent Elements
// Return the k most frequent elements.
// Approach: group by value, order by count descending, take k.
// O(n log n) with LINQ ordering.
public class TopKFrequent : IPuzzle
{
    public string Name => "Top K Frequent";
    public string Description => "LC #347 — k most frequent elements";
    public string Explanation =>
        """
        Build a frequency map by grouping identical values together and counting each group.
        Sort the groups by count descending, take the first k, and return their keys.
        Same frequency map pattern as Three of a Kind — just ordered and sliced instead of filtered.

        Performance:
          Time:  O(n log n) — dominated by sorting the frequency groups.
          Space: O(n) — frequency map holds up to n distinct values.
        """;

    public void Run()
    {
        Console.WriteLine(string.Join(", ", Solve([1, 1, 1, 2, 2, 3], 2)));        // 1, 2
        Console.WriteLine(string.Join(", ", Solve([1], 1)));                        // 1
        Console.WriteLine(string.Join(", ", Solve([4, 1, -1, 2, -1, 2, 3], 2)));   // -1, 2
    }

    private static int[] Solve(int[] nums, int k) =>
        [.. nums.GroupBy(n => n)
                .OrderByDescending(g => g.Count())
                .Take(k)
                .Select(g => g.Key)];
}
