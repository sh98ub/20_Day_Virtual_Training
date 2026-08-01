using System;

namespace Q9_ArithmeticExpressions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] testCases = {
                "10 + 5",
                "10 - 3",
                "4 * 5",
                "20 / 4",
                "10 / 0",
                "abc + 5",
                "10 % 5",
                "10 +",
                "10  + 5"
            };

            foreach (var exp in testCases)
            {
                Console.WriteLine($"\"{exp}\" => {EvaluateExpression(exp)}");
            }
        }

        public static string EvaluateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return "Error:InvalidExpression";

            string[] parts = expression.Split(' ');
            if (parts.Length != 3)
                return "Error:InvalidExpression";

            if (!int.TryParse(parts[0], out int a) || !int.TryParse(parts[2], out int b))
                return "Error:InvalidNumber";

            string op = parts[1];

            return op switch
            {
                "+" => (a + b).ToString(),
                "-" => (a - b).ToString(),
                "*" => (a * b).ToString(),
                "/" => b == 0 ? "Error:DivideByZero" : (a / b).ToString(),
                _ => "Error:UnknownOperator"
            };
        }
    }
}
