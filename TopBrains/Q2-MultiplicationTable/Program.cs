using System;

namespace Q2_MultiplicationTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter n: ");
            int n = int.Parse(Console.ReadLine()!);

            Console.Write("Enter upto: ");
            int upto = int.Parse(Console.ReadLine()!);

            int[] result = GetMultiplicationTable(n, upto);

            Console.WriteLine($"Result: [{string.Join(", ", result)}]");
        }

        static int[] GetMultiplicationTable(int n, int upto)
        {
            if (upto <= 0) return Array.Empty<int>();

            int[] row = new int[upto];
            for (int i = 1; i <= upto; i++)
            {
                row[i - 1] = n * i;
            }
            return row;
        }
    }
}
