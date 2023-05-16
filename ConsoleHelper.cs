using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    public static class ConsoleHelper
    {
        // Prompts the user for a string input and returns the input if it is not null, empty or whitespace.
        // If the input is invalid, the method displays an error message and prompts the user again.
        public static string PromptForString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid value.");
                }
            }
        }
        // Prompts the user for a long integer input and returns the input if it is a valid long integer.
        // If the input is invalid, the method displays an error message and prompts the user again.
        public static long PromptForLong(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (long.TryParse(input, out long result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid long value.");
                }
            }
        }
    }
}
