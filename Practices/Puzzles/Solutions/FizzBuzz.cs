namespace Puzzles.Solutions;

// FizzBuzz 1–50. The trap: checking "both" as a separate case before the others,
// or nesting ifs. The clean read: build the label by concatenation — if it's empty, use the number.
public class FizzBuzz : IPuzzle
{
    public string Name => "FizzBuzz";
    public string Description => "Multiples of 3→Fizz, 5→Buzz, both→FizzBuzz, else number";
    public string Explanation =>
        """
        The trap is adding a separate branch for "both" — that's three conditions when two are enough.
        Build the label by concatenating "Fizz" and "Buzz" independently.
        A multiple of 15 naturally produces "FizzBuzz" without any extra check.
        If the label is still empty, print the number.

        Performance:
          Time:  O(n) — one pass from 1 to n; each iteration is O(1).
          Space: O(1) — no extra structures, just the label string per iteration.
        """;

    public void Run()
    {
        Console.WriteLine("Loop:");
        for (int i = 1; i <= 50; i++)
        {
            string label = (i % 3 == 0 ? "Fizz" : "") + (i % 5 == 0 ? "Buzz" : "");
            Console.WriteLine(label != "" ? label : i);
        }

        Console.WriteLine("\nLINQ:");
        Enumerable.Range(1, 50)
                  .Select(i => (i % 3, i % 5) switch
                  {
                      (0, 0) => "FizzBuzz",
                      (0, _) => "Fizz",
                      (_, 0) => "Buzz",
                      _      => $"{i}"
                  })
                  .ToList().ForEach(Console.WriteLine);
    }
}
