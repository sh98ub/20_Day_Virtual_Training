using System;

class Program
{
    static void Main()
    {
        double length, width, height;

        while (true)
        {
            Console.Write("Enter Length: ");

            if (double.TryParse(Console.ReadLine(), out length) && length > 0)
                break;

            Console.WriteLine("Invalid Length.");
        }

        while (true)
        {
            Console.Write("Enter Width: ");

            if (double.TryParse(Console.ReadLine(), out width) && width > 0)
                break;

            Console.WriteLine("Invalid Width.");
        }

        while (true)
        {
            Console.Write("Enter Height: ");

            if (double.TryParse(Console.ReadLine(), out height) && height > 0)
                break;

            Console.WriteLine("Invalid Height.");
        }

        double volume = length * width * height;

        Console.WriteLine($"\nVolume = {Math.Round(volume,2)}");
    }
}