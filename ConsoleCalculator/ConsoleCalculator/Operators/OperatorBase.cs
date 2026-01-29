using ConsoleCalculator.Configuration;
using ConsoleCalculator.Helpers;
using System.Collections.Generic;

namespace ConsoleCalculator.Operators
{
    public class OperatorBase
    {
        protected List<int> GetNumbers(string rawString, ref List<string> errors)
        {
            //Step 1: we split the string by delimeters
            string[] values = DelimitersParser.GetSplittedValuesByDelimeters(rawString);

            //Step 2: Validate input if all are numbers and other rules
            ValidateInput(rawString, values, ref errors);

            //Step 3: Convert values to numbers applying conversion rules
            return GetNumberListAfterConvertionRules(values, ref errors);
        }

        private void ValidateInput(string rawString, string[] values, ref List<string> errors)
        {
            // We don't have a limit of two numbers when we have custom delimiters
            if (!rawString.StartsWith("//"))
            {
                // Requirement 1
                if (FeatureToggles.IsSupportMaxTwoNumbersEnabled)
                    ApplySupportTwoNumbersRules(values, ref errors);
               else
                    errors.AddRange(ValidationValues.AreAllNumbersValid(values));
            }

            // Requirement 4: Deny negative numbers
            if (FeatureToggles.IsDenyNegativeNumbersEnabled)
            {
                errors.AddRange(ValidationValues.NoNegativeNumbers(values));
            }
        }

        private List<int> GetNumberListAfterConvertionRules(string[] values, ref List<string> errors)
        {
            if (errors.Count > 0)
            {
                return new List<int>();
            }
            List<int> listNumbers = new List<int>();
            foreach (var value in values)
            {
                if (FeatureToggles.IsSupportMaxTwoNumbersEnabled || FeatureToggles.IsGreaterThan1000Enabled)
                {
                    listNumbers.Add(ApplyConvertionNumberRules(value));
                }
                else
                {
                    listNumbers.Add(int.Parse(value)); //we validated before that all values are numbers
                }
            }
            return listNumbers;
        }

        #region Validation Methods
        private void ApplySupportTwoNumbersRules(string[] stringValues, ref List<string> errors)
        {
            //requirement 2
            if (FeatureToggles.IsRemoveMaxRuleForNumbersEnabled)
            {
                if (!ValidationValues.AreAllNumbers(stringValues)) //no max constraint for numbers
                {
                    errors.AddRange(ValidationValues.NoMoreThanTwoNumbers(stringValues));
                }
            }
            else
            {
                errors.AddRange(ValidationValues.NoMoreThanTwoNumbers(stringValues));
            }
        }
        private int ApplyConvertionNumberRules(string value)
        {
            int numberParsed = 0;
            //requirement 1
            if (FeatureToggles.IsSupportMaxTwoNumbersEnabled)
                numberParsed = ConvertionToNumber.ParseToNumberConvertingInvalidsToZero(value);
            //requirement 5
            if (FeatureToggles.IsGreaterThan1000Enabled)
                numberParsed = ConvertionToNumber.IfGreaterThan1000Then0(numberParsed);

            return numberParsed;
        }
        #endregion
       
    }
}
