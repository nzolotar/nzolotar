using System;

namespace ConsoleApp_StackProblem
{
    //create a class implemeting stack with Push, Pop and peek

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Create a new stack
            CustomStack<int> stack = new CustomStack<int>();

            // Push some items
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            // Peek at top item
            Console.WriteLine(stack.Peek());  // Output: 3

            // Pop items
            int item = stack.Pop();  // item = 3
            Console.WriteLine(item);  // Output: 3

            // Check if stack is empty
            Console.WriteLine(stack.IsEmpty);  // Output: False

            // Get current count
            Console.WriteLine(stack.Count);  // Output: 2

            // Using TryPop and TryPeek
            if (stack.TryPeek(out int peekResult))
            {
                Console.WriteLine($"Top item is: {peekResult}");
            }
            if (stack.TryPop(out int popResult))
            {
                Console.WriteLine($"Popped item: {popResult}");
            }

            // Create a stack with max size
            CustomStack<string> boundedStack = new CustomStack<string>(maxSize: 3);
        }
    }
}
