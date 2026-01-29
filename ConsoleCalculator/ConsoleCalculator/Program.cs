using System;
using System.Collections.Generic;
using ConsoleCalculator.Operators;

namespace ConsoleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> errors = new List<string>();
            Console.WriteLine("Welcome to Console Calculator!");
            Console.WriteLine("Press Ctrl+C to exit the application.");
            Console.WriteLine();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the string:");
                    string rawString = Console.ReadLine();
                    Console.WriteLine("Select operation (+, -, *, /):");
                    string operation = Console.ReadLine();
                    IOperator operatorInstance = OperatorManager.Create(operation);
                    if (operatorInstance == null)
                    {
                        Console.WriteLine($"Error: Invalid operation '{operation}'");
                        Console.WriteLine();
                        continue;
                    }
                    List<int> listNumbers = new List<int>();
                    int result = operatorInstance.GetResult(rawString, ref errors, ref listNumbers);
                    if (errors.Count > 0)
                    {
                        ShowErrors(errors);
                        errors.Clear();
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        //show the listNumbers values in line separated by operation
                        Console.WriteLine($"{string.Join(operation, listNumbers)} = {result}");
                        Console.WriteLine();
                    }   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine();
                }
            }
        }

        private static void ShowErrors(List<string> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            errors.Clear();
            Console.WriteLine();
        }
    }
}
