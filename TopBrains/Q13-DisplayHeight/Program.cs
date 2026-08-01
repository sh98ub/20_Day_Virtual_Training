using System;

namespace Q13_DisplayHeight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter height in cm: ");
            int heightCm = int.Parse(Console.ReadLine()!);

            string category = GetHeightCategory(heightCm);
            Console.WriteLine($"Category: {category}");
        }

        public static string GetHeightCategory(int heightCm)
        {
            if (heightCm < 150)
                return "Short";
            else if (heightCm < 180)
                return "Average";
            else
                return "Tall";
        }
    }
}
