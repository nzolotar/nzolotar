/*
 * Palindrome Check
 *
 * Problem:
 *   Given a string, return true if it reads the same forward and backward.
 *   The comparison must be case-insensitive and must ignore all non-alphanumeric characters.
 *   An empty or whitespace-only string returns false.
 *   Example: "Draw, o coward!" → true  (punctuation and spaces stripped → "drawocoward")
 */

namespace code_challenge;

public static class Palindrome
{
    /// <summary>
    /// Should return true if the value is a palindrome. (reads the same forward and backward)
    /// The comparison should be case-insensitive and should exclude non-alphanumeric characters.
    /// example: "Draw, o coward!" is a palindrome.
    /// </summary>
    public static bool IsPalindrome(string value) 
    {
        //validate string 
        if (string.IsNullOrWhiteSpace(value))
          return false;

        //exlude non-apha, covert to array of chars, run isnumberorcharacter, retun only if isnumberorcharacter = true 
        var cleaned = value.ToLowerInvariant().ToArray().Where( x => char.IsLetterOrDigit(x) == true); 


        // reverse and compare 
        return cleaned.ToArray().Reverse().SequenceEqual(cleaned);
    }
}