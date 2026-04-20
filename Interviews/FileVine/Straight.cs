/*
 * Straight (Consecutive Integers)
 *
 * Problem:
 *   Given a list of integers, return true if the list contains four or more
 *   distinct values that form a consecutive integer sequence, regardless of order.
 *   Examples:
 *     [1,3,6,2,4] → true   (1,2,3,4 are four consecutive distinct integers)
 *     [6,2,3,5,1] → false  (no run of four consecutive integers exists)
 */

namespace filevine_code_challenge;

public static class Straight
{
    /// <summary>
    /// Returns true if the input contains four or more distinct consecutive integers,
    /// regardless of their order in the list.
    ///
    /// Examples:
    /// [1,3,6,2,4] = true
    /// [6,2,3,5,1] = false
    /// </summary>
    public static bool IsStraight(List<int> input) 
    {
        var sortedDistinct = input.Distinct().OrderBy(x => x).ToList();

        if (sortedDistinct.Count < 4) return false;

        // Slide a 4-element window over the sorted distinct values.
        // If the last element minus the first equals 3, all four must be consecutive
        // (guaranteed because values are distinct integers and sorted).
        for (int i = 0; i <= sortedDistinct.Count - 4; i++)
        {
            if (sortedDistinct[i + 3] - sortedDistinct[i] == 3)
                return true;
        }

        return false;
    }
}
