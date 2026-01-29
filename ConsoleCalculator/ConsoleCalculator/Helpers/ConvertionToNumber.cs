using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Helpers
{
    public static class ConvertionToNumber
    {
        public static int ParseToNumberConvertingInvalidsToZero(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            if (int.TryParse(value.Trim(), out int result))
                return result;

            return 0;
        }

        public static int IfGreaterThan1000Then0(int number)
        {
            return number > 1000 ? 0 : number;
        }
    }
}
