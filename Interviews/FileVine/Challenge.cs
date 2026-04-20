/*
 * Array Scoring Problems
 *
 * Problem 1 — SumOfPositiveValues:
 *   Given a list of integers, return the sum of all values that are greater than zero.
 *   An empty list returns 0.
 *
 * Problem 2 — ContainsThreeOrMoreOfAKind:
 *   Given a list of integers, check whether any single value appears three or more times.
 *   If yes, return the sum of all positive values in the list.
 *   If no, return 0.
 *   Examples:
 *     [1,2,3,4,5]    → 0   (no value repeats 3+ times)
 *     [2,2,3,3,2]    → 12  (2 appears 3 times; sum of positives = 2+2+3+3+2)
 *     [-1,2,3,-1,-1] → 5   (-1 appears 3 times; sum of positives = 2+3)
 */

namespace filevine_code_challenge;
public static class Challenge
{
    /// <summary>
    /// Returns the sum of all values in input where value is greater than zero.
    /// </summary>
    public static int SumOfPositiveValues(List<int> input)
    {
        //TODO: Implement this method.  To run the tests, run `dotnet test` in the terminal.
        if (input.Count() == 0)
         return 0;

        return input.Where(x => x > 0).Sum();
    }

    /// <summary>
    /// if input[] contains three (or more) of the same value,
    ///   return the sum of all values in input where value is greater than zero.
    /// else
    ///   return 0
    /// example: [1,2,3,4,5] = 0
    /// example: [2,2,3,3,2] = 12
    /// example: [6,2,3,2,5] = 0
    /// example: [-1,2,3,-1,-1] = 5
    /// </returns>
    public static int ContainsThreeOrMoreOfAKind(List<int> input)
    {
      if (input.Count() == 0)
         return 0;

       bool hasThreeOrMore = input
        .GroupBy(x => x)
        .Any(g => g.Count() >= 3);

      if (hasThreeOrMore)
        return input.Where(x => x > 0).Sum();

       return 0; 
    }
}