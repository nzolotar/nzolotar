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
        """;

    public void Run()
    {
        Solve([2, 7, 4, 1, 3, 6], 8);   // 1 7, 2 6
        Solve([1, 5, 3, 2, 4], 6);       // 1 5, 2 4
        Solve([1, 2, 3], 10);            // (none)
    }

    private static void Solve(int[] nums, int target)
    {
        Array.Sort(nums);
        int left = 0, right = nums.Length - 1;

        Console.WriteLine($"Input: [{string.Join(", ", nums)}]  Target: {target}");

        bool found = false;
        while (left < right)
        {
            int sum = nums[left] + nums[right];
            if (sum == target)
            {
                Console.WriteLine($"  {nums[left]} {nums[right]}");
                found = true;
                left++;
                right--;
            }
            else if (sum < target) left++;
            else right--;
        }

        if (!found) Console.WriteLine("  (no pairs found)");
        Console.WriteLine();
    }
}
