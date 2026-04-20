namespace Puzzles.Solutions;

// Given a list of integers, find all numbers appearing exactly three times.
// Approach: frequency map — group by value, filter where count == 3.
// O(n) time, O(n) space.
public class ThreeOfAKind : IPuzzle
{
    public string Name => "Three of a Kind";
    public string Description => "Find all numbers appearing exactly three times";
    public string Explanation =>
        """
        Group the numbers by value to build a frequency map, then filter to groups
        whose count is exactly 3. One LINQ chain — GroupBy, Where, Select.
        """;

    public void Run()
    {
        Solve([1, 2, 3, 2, 1, 3, 3, 1, 2, 4]);  // 1 2 3
        Solve([5, 5, 5, 6, 6, 7]);               // 5
        Solve([1, 2, 3, 4]);                     // (none)
    }

    private static void Solve(int[] nums)
    {
        var result = nums.GroupBy(n => n)
                         .Where(g => g.Count() == 3)
                         .Select(g => g.Key)
                         .ToList();

        Console.WriteLine($"Input:  [{string.Join(", ", nums)}]");
        Console.WriteLine($"Output: {(result.Count > 0 ? string.Join(", ", result) : "(none)")}");
        Console.WriteLine();
    }
}
