using System.Collections.Generic;
using System.Linq;

namespace ConsoleCalculator.Operators
{
    public class Addition : OperatorBase, IOperator
    {
        public Addition()
        {
        }

        public int GetResult(string rawString, ref List<string> errors, ref List<int> listNumbers)
        {
            listNumbers = GetNumbers(rawString, ref errors);
            if (errors.Count > 0)
            {
                return 0;
            }
            return listNumbers.Sum();
        }




    }
}
