namespace ConsoleCalculator.Operators
{
    public static class OperatorManager
    {
        public static IOperator Create(string operation)
        {
            switch (operation)
            {
                case "+":
                    return new Addition();
                case "-":
                    return null;
                //return new Subtraction();
                case "*":
                    return null;
                //return new Multiplication();
                case "/":
                    return null;
                // return new Division();
                default:
                    return null;
            }
        }
    }
}
