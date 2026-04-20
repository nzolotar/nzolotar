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
        """;

    public void Run()
    {
        Console.WriteLine(string.Join(", ", Solve([2, 7, 11, 15], 9)));  // 0, 1
        Console.WriteLine(string.Join(", ", Solve([3, 2, 4], 6)));       // 1, 2
        Console.WriteLine(string.Join(", ", Solve([3, 3], 6)));          // 0, 1
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
}
