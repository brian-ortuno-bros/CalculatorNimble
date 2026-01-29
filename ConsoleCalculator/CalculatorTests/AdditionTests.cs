using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ConsoleCalculator.Operators;

namespace CalculatorTests
{
    [TestClass]
    public class AdditionTests
    {
        #region Requirement 1 examples
        [TestMethod]
        public void Addition_SingleNumber_ReturnsSameNumber()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("20", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(20, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_TwoNumbers_ReturnsSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("1,5000", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(5001, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_InvalidString_TreatedAsZero()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("5,tytyt", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(5, result); // tytyt converts to 0
            Assert.AreEqual(0, errors.Count, "Should have no errors (invalid strings convert to 0)");
        }

        [TestMethod]
        public void Addition_EmptyString_ReturnsZero()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_OnlyZeros_ReturnsZero()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("0,0,0", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_WhitespaceHandling_ReturnsCorrectSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult(" 1 , 2 , 3 ", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(6, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_NegativeNumbers_Requirement1_example()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("4,-3", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(1, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }
        #endregion

        #region Requirement 2 examples
        [TestMethod]
        public void Addition_MultipleNumbers_ReturnsCorrectSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("1,2,3,4,5,6,7,8,9,10,11,12", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(78, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        #endregion
        #region Requirement 3 - Newline as Delimiter

        [TestMethod]
        public void Addition_NewlineAndCommaDelimiters_ReturnsCorrectSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("1\n2,3", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(6, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        #endregion

        #region Requierement 4 - Negative Numbers

        [TestMethod]
        public void Addition_NegativeNumbers_ReturnsErrorInList()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("4,-3", ref errors, ref listNumbers);

            // Assert
            Assert.IsTrue(errors.Count > 0, "Should have errors for negative numbers");
            Assert.IsTrue(errors[0].Contains("-3"), "Error message should contain the negative number");
            Assert.IsTrue(errors[0].Contains("Negative numbers are not allowed"), 
                "Error message should indicate negative numbers are not allowed");
        }

        [TestMethod]
        public void Addition_MultipleNegativeNumbers_ReturnsAllNegativesInError()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("1,-2,-3,4", ref errors, ref listNumbers);

            // Assert
            Assert.IsTrue(errors.Count > 0, "Should have errors for negative numbers");
            Assert.IsTrue(errors[0].Contains("-2"), "Error should contain first negative number");
            Assert.IsTrue(errors[0].Contains("-3"), "Error should contain second negative number");
        }

        #endregion

        #region Conversion Rules Tests

        [TestMethod]
        public void Addition_NumberGreaterThan1000_TreatedAsZero()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("2,1001,6", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(8, result); // 2 + 0 (1001>1000) + 6
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_NumberEqualTo1000_IncludedInSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("2,1000,6", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(1008, result); // 1000 is valid
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        #endregion

        #region Requirement 6 Custom Single Character Delimiter Tests

        [TestMethod]
        public void Addition_CustomSingleCharDelimiter_Hash_ReturnsCorrectSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("//#\n2#5", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(7, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_CustomSingleCharDelimiter_WithInvalidNumber_ReturnsCorrectSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("//,\n2,ff,100", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(102, result); // 2 + 0 (ff) + 100
            CollectionAssert.AreEqual(new List<int> { 2, 0, 100 }, listNumbers, "List of numbers should reflect conversion rules");
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        #endregion

        #region Requirement 7 Custom String Delimiter Tests

        [TestMethod]
        public void Addition_CustomStringDelimiter_ThreeStars_ReturnsCorrectSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("//[***]\n11***22***33", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(66, result);
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        #endregion

        #region Requirement 8 Multiple Custom Delimiters Tests

        [TestMethod]
        public void Addition_MultipleCustomDelimiters_ReturnsCorrectSum()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("//[*][!!][r9r]\n11r9r22*hh*33!!44", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(110, result); // 11 + 22 + 0 (hh) + 33 + 44
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_ComplexScenario_CustomDelimiterWithConversionRules()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("//[***]\n1***2***1001***3", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(6, result); // 1 + 2 + 0 (1001>1000) + 3
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        [TestMethod]
        public void Addition_ComplexScenario_MultipleDelimitersWithInvalidAndLargeNumbers()
        {
            // Arrange
            Addition addition = new Addition();
            List<string> errors = new List<string>();
            List<int> listNumbers = new List<int>();

            // Act
            int result = addition.GetResult("//[*][%]\n5*invalid%10*2000%3", ref errors, ref listNumbers);

            // Assert
            Assert.AreEqual(18, result); // 5 + 0 (invalid) + 10 + 0 (2000>1000) + 3
            Assert.AreEqual(0, errors.Count, "Should have no errors");
        }

        #endregion
    }
}
