using System;

namespace Q1_Swapping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter First Number: ");
            int a = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Second Number: ");
            int b = int.Parse(Console.ReadLine()!);

            Console.WriteLine($"\nBefore Swap: a = {a}, b = {b}");

            // Method 1: ref
            int x = a, y = b;
            SwapRef(ref x, ref y);
            Console.WriteLine($"Method 1 (ref) -> After Swap: a = {x}, b = {y}");

            // Method 2: out
            SwapOut(a, b, out int p, out int q);
            Console.WriteLine($"Method 2 (out) -> After Swap: a = {p}, b = {q}");
        }

        static void SwapRef(ref int a, ref int b)
        {
            a = a + b;
            b = a - b;
            a = a - b;
        }

        static void SwapOut(int a, int b, out int swappedA, out int swappedB)
        {
            swappedA = a + b;
            swappedB = swappedA - b;
            swappedA = swappedA - swappedB;
        }
    }
}
