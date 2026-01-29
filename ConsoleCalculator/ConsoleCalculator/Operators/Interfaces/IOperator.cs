using System.Collections.Generic;

namespace ConsoleCalculator.Operators
{
    public interface IOperator
    {
        int GetResult(string rawString, ref List<string> errors, ref List<int> listNumbers);
    }
}
