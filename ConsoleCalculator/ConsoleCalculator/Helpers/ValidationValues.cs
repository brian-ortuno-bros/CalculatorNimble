using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Helpers
{
    public static class ValidationValues
    {
        public static string[] NoMoreThanTwoNumbers(string[] stringValues)
        {
            if (stringValues.Length > 2)
            {
                return new[] { $"Error: More than two numbers are not allowed. Found {stringValues.Length} numbers." };
            }

            return new string[0]; // No errors
        }

        public static bool AreAllNumbers(string[] stringValues)
        {
            foreach (var value in stringValues)
            {
                if (!int.TryParse(value.Trim(), out _))
                    return false;
            }

            return true;
        }

        public static string[] AreAllNumbersValid(string[] stringValues)
        {
            if (!AreAllNumbers(stringValues))
            {
                return new[] { $"Error: All the values should be numbers" };
            }

            return new string[0]; // No errors
        }

        public static string[] NoNegativeNumbers(string[] stringValues)
        {
            List<int> negativeNumbers = new List<int>();

            foreach (var value in stringValues)
            {
                if (int.TryParse(value.Trim(), out int num) && num < 0)
                {
                    negativeNumbers.Add(num);
                }
            }

            if (negativeNumbers.Count > 0)
            {
                return new[] { $"Error: Negative numbers are not allowed: {string.Join(", ", negativeNumbers)}" };
            }

            return new string[0]; // No errors
        }
    }
}
