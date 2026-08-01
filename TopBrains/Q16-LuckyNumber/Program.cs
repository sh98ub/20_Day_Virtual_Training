using System;

namespace Q16_LuckyNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter ticket number (x): ");
            if (long.TryParse(Console.ReadLine(), out long x))
            {
                bool isLucky = IsLuckyNumber(x);
                Console.WriteLine($"Is {x} a Lucky Number? {isLucky}");
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }

        public static bool IsLuckyNumber(long x)
        {
            if (x <= 0 || IsPrime(x))
                return false;

            long sumX = SumOfDigits(x);
            long squareX = x * x;
            long sumSquareX = SumOfDigits(squareX);

            return sumSquareX == (sumX * sumX);
        }

        public static bool IsPrime(long n)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0 || n % 3 == 0) return false;

            for (long i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
            }
            return true;
        }

        public static long SumOfDigits(long n)
        {
            long sum = 0;
            n = Math.Abs(n);
            while (n > 0)
            {
                sum += n % 10;
                n /= 10;
            }
            return sum;
        }
    }
}
