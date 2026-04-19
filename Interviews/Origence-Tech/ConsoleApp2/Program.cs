using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("1:" + RemoveDuplicates("IXYZZI")); // IXYZ
            Console.WriteLine("2:" + MostEfficient("IXYZZI")); // IXYZ
            Console.WriteLine("3:" + RemoveDuplicatesLinq("IXYZZI")); // IXYZ
            Console.WriteLine("4:" + RemoveDuplicatesWithCount("IXYZZI")); // IXYZ
            Console.Read();
        }

        //Using HashSet (maintains order of first occurrence)
        public static string MostEfficient(string word) {
            HashSet<char> uniqueChars = new HashSet<char>(word);
            return new string(uniqueChars.ToArray());
        }
        //Using LINQ (shorter but might be less performant)
        public static string RemoveDuplicatesLinq(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return new string(input.Distinct().ToArray());
        }
        // Using Dictionary
        public static string RemoveDuplicatesWithCount(string input)
        {
            var charCount = new Dictionary<char, int>();
            if (string.IsNullOrEmpty(input)) return input;

            var result = new StringBuilder();

            foreach (char c in input)
            {
                if (charCount.ContainsKey(c))
                {
                    charCount[c]++;
                }
                else
                {
                    charCount.Add(c, 1);
                    result.Append(c);
                }
            }

            return result.ToString();
        }
        public static string RemoveDuplicates(string word)
        {
            //verify if string is not blank 
            if (string.IsNullOrWhiteSpace(word))
                throw new Exception("No word work with");

            List<char> processedCharacters = new List<char>();
            List<char> result = new List<char>();

            foreach (char character in word)
            {
                // find if it is in processedCharacters
                var isItProcessed = processedCharacters.Contains(character);

                if (!isItProcessed)
                {
                    result.Add(character);
                    processedCharacters.Add(character);
                }

                //if processed and
                if (isItProcessed)
                {                    
                    processedCharacters.Add(character);
                }
            }
            return string.Concat(result);
        }
    }
}
