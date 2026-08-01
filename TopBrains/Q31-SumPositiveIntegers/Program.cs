using System;

namespace Q31_SumPositiveIntegers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 5, -2, 10, -3, 15, 0, 20, -5 };
            long sum = SumPositiveUntilZero(nums);

            Console.WriteLine($"Input: [{string.Join(", ", nums)}]");
            Console.WriteLine($"Sum:   {sum}");
        }

        public static long SumPositiveUntilZero(int[] nums)
        {
            if (nums == null) return 0;

            long sum = 0;
            foreach (int n in nums)
            {
                if (n == 0)
                    break;
                if (n < 0)
                    continue;

                sum += n;
            }

            return sum;
        }
    }
}
