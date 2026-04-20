namespace Puzzles.Solutions;

// LC #1 — Two Sum
// Given an array and a target, return indices of the two numbers that sum to target.
// Approach: hash map — store each number's index; on each step check if complement exists.
// O(n) time, O(n) space.
public class TwoSum : IPuzzle
{
    public string Name => "Two Sum";
    public string Description => "LC #1 — indices of two numbers that sum to target";
    public string Explanation =>
        """
        Use a hash map to store each number and its index as you iterate.
        For every number, check if its complement (target - current) is already in the map.
        If yes — you found the pair. If no — store the current number and move on.
        This avoids the O(n²) brute force of checking every pair.

        Performance:
          Time:  O(n) — single pass through the array.
          Space: O(n) — up to n entries in the hash map.
        """;

    public void Run()
    {
        int[][] cases = [[2, 7, 11, 15], [3, 2, 4], [3, 3]];
        int[] targets = [9, 6, 6];

        Console.WriteLine("Hash map — O(n):");
        for (int t = 0; t < cases.Length; t++)
            Console.WriteLine($"  [{string.Join(", ", Solve(cases[t], targets[t]))}]");

        Console.WriteLine("\nLINQ — O(n²):");
        for (int t = 0; t < cases.Length; t++)
            Console.WriteLine($"  [{string.Join(", ", SolveLinq(cases[t], targets[t]))}]");
    }

    private static int[] Solve(int[] nums, int target)
    {
        var seen = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (seen.TryGetValue(target - nums[i], out int j))
                return [j, i];
            seen[nums[i]] = i;
        }
        return [];
    }

    // Shorter but O(n²) — checks every pair
    private static int[] SolveLinq(int[] nums, int target) =>
        Enumerable.Range(0, nums.Length)
                  .SelectMany(i => Enumerable.Range(i + 1, nums.Length - i - 1)
                                             .Where(j => nums[i] + nums[j] == target)
                                             .Select(j => new[] { i, j }))
                  .FirstOrDefault() ?? [];
}
