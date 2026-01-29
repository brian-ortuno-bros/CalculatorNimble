using ConsoleCalculator.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleCalculator.Operators
{
    public static class DelimitersParser
    {
        public static string[] GetSplittedValuesByDelimeters(string rawString)
        {
            string[] valuesSplitted = null;
            if (rawString.StartsWith("//")) //custom delimiter case
            {
                //requirement 6
                if (FeatureToggles.IsSupportCustomSingleCharDelimiterEnabled)
                {
                    var result = SplitBySingleCustomCharDelimiter(rawString);
                    if (result != null) { valuesSplitted = result; }
                }
                //requirement 7
                if (FeatureToggles.IsSupportCustomManyCharDelimiterEnabled)
                {
                    var result = SplitBySingleCustomManyCharDelimiter(rawString);
                    if (result != null) { valuesSplitted = result; }
                }

                //requirement 8
                if (FeatureToggles.IsSupportCustomMultipleDelimiterEnabled)
                {
                    var result = SplitByMultipleCustomStringDelimiters(rawString);
                    if (result != null) { valuesSplitted = result; }
                }
            }
            if (valuesSplitted == null)
            {
                valuesSplitted = SplitByCharDelimiters(rawString);
            }
            return valuesSplitted;
        }

        #region Private Split Methods
        private static string[] SplitByCharDelimiters(string rawString)
        {
            char newLineChar = '\n';
            char defaultChar = ','; //comma as default delimiter
            char[] delimiters = FeatureToggles.IsSupportNewLineDelimiterEnabled  //requirement 3
                    ? new[] { defaultChar, newLineChar }
                    : new[] { defaultChar };

            return rawString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                           .Select(s => s.Trim())
                           .ToArray();
        }

        private static string[] SplitBySingleCustomCharDelimiter(string rawString)
        {
            string pattern = @"^//(.)(\\n|\n)";
            Match match = Regex.Match(rawString, pattern);

            if (match.Success)
            {
                // Extract the custom delimiter from group 1 i.e  //#\n2#5 the custom delimiter is #
                char customDelimiter = match.Groups[1].Value[0];

                // Find where the numbers start (after the matched pattern)
                int numbersStartIndex = match.Length;
                string numbersString = rawString.Substring(numbersStartIndex);

                // Split by the custom delimiter
                return numbersString.Split(customDelimiter)
                        .Select(s => s)
                        .ToArray();
            }
            return null;
        }

        private static string[] SplitBySingleCustomManyCharDelimiter(string rawString)
        {
            string pattern = @"^//\[([^\]]+)\](\\n|\n)";
            Match match = Regex.Match(rawString, pattern);

            if (match.Success)
            {
                // Extract the custom delimiter from group 1
                string customDelimiter = match.Groups[1].Value;

                // Find where the numbers start (after the matched pattern)
                int numbersStartIndex = match.Length;
                string numbersString = rawString.Substring(numbersStartIndex);

                return numbersString.Split(new[] { customDelimiter }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s)
                        .ToArray();
            }
            return null;
        }

        private static string[] SplitByMultipleCustomStringDelimiters(string rawString)
        {
            // Pattern to match the entire format: //[delim1][delim2]...\n or \\n
            string pattern = @"^//(\[([^\]]+)\])+?(\\n|\n)";
            Match match = Regex.Match(rawString, pattern);

            if (!match.Success)
                return null;

            // Find where the numbers start (after the matched pattern)
            int numbersStartIndex = match.Length;
            string numbersString = rawString.Substring(numbersStartIndex);

            // Extract all delimiters between brackets using regex
            string delimiterPattern = @"\[([^\]]+)\]";
            MatchCollection delimiterMatches = Regex.Matches(rawString.Substring(0, numbersStartIndex), delimiterPattern);

            if (delimiterMatches.Count == 0)
                return null;

            // i.e //[*][!!][r9r]\n11r9r22*hh*33!!44 delimiters are *, !!, r9r
            List<string> delimiters = delimiterMatches.Cast<Match>()
                         .Select(m => m.Groups[1].Value).ToList();

            // Split the numbers part by multiple delimiters
            string[] resultValues = new[] { numbersString };

            foreach (string delimiter in delimiters)
            {
                List<string> temp = new List<string>();
                foreach (string part in resultValues)
                {
                    string[] splitParts = part.Split(new[] { delimiter }, StringSplitOptions.None);
                    temp.AddRange(splitParts);
                }
                resultValues = temp.ToArray();
            }

            return resultValues;
        }
        #endregion
    }
}
