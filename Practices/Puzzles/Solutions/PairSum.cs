namespace Puzzles.Solutions;

// Given a list of integers, find all pairs that sum to a target. Print each pair once in ascending order.
// Approach: sort + two pointers — move left up or right down based on current sum vs target.
// O(n log n) time, O(1) space.
public class PairSum : IPuzzle
{
    public string Name => "Pair Sum";
    public string Description => "Find all pairs that sum to a target value";
    public string Explanation =>
        """
        Sort the array, then place one pointer at the start and one at the end.
        If the two values sum to the target — record the pair and move both pointers inward.
        If the sum is too small — move the left pointer right to increase it.
        If the sum is too large — move the right pointer left to decrease it.
        Sorting guarantees each pair is printed in ascending order with no duplicates.

        Performance:
          Time:  O(n log n) — dominated by the sort; the two-pointer scan is O(n).
          Space: O(1) — sorting in place, no extra structures.
        """;

    public void Run()
    {
        (int[] nums, int target)[] cases =
        [
            ([2, 7, 4, 1, 3, 6], 8),
            ([1, 5, 3, 2, 4],    6),
            ([1, 2, 3],         10),
        ];

        Console.WriteLine("Two pointers — O(n log n):");
        foreach (var (nums, target) in cases)
            Solve([.. nums], target);

        Console.WriteLine("LINQ — O(n²):");
        foreach (var (nums, target) in cases)
        {
            var pairs = SolveLinq(nums, target);
            Console.WriteLine(pairs.Any()
                ? string.Join("  ", pairs.Select(p => $"{p.a} {p.b}"))
                : "  (no pairs found)");
        }
    }

    private static void Solve(int[] nums, int target)
    {
        Array.Sort(nums);
        int left = 0, right = nums.Length - 1;
        bool found = false;

        while (left < right)
        {
            int sum = nums[left] + nums[right];
            if (sum == target) { Console.WriteLine($"  {nums[left]} {nums[right]}"); found = true; left++; right--; }
            else if (sum < target) left++;
            else right--;
        }

        if (!found) Console.WriteLine("  (no pairs found)");
    }

    // Shorter but O(n²) — checks every combination
    private static IEnumerable<(int a, int b)> SolveLinq(int[] nums, int target) =>
        nums.SelectMany((a, i) => nums.Skip(i + 1)
                                      .Where(b => a + b == target)
                                      .Select(b => (Math.Min(a, b), Math.Max(a, b))))
            .Distinct();
}
