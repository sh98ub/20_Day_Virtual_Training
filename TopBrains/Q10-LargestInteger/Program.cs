using System;

namespace Q10_LargestInteger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a: ");
            int a = int.Parse(Console.ReadLine()!);

            Console.Write("Enter b: ");
            int b = int.Parse(Console.ReadLine()!);

            Console.Write("Enter c: ");
            int c = int.Parse(Console.ReadLine()!);

            int max = FindLargest(a, b, c);
            Console.WriteLine($"Largest: {max}");
        }

        public static int FindLargest(int a, int b, int c)
        {
            if (a >= b && a >= c)
                return a;
            else if (b >= c)
                return b;
            else
                return c;
        }
    }
}
