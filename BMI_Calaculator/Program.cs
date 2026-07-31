using System;

class Program
{
    static void Main()
    {
        double weight, height;

        Console.Write("Enter Weight (kg)");
        while (!double.TryParse(Console.ReadLine(), out weight) || weight <= 0)
        {
            Console.Write("Invalid Weight. Enter again: ");
        }

        Console.Write("Enter Height (m): ");
        while (!double.TryParse(Console.ReadLine(), out height) || height <= 0)
        {
            Console.Write("Invalid Height. Enter again: ");
        }

        double bmi = weight / (height * height);

        Console.WriteLine($"\nBMI = {Math.Round(bmi, 2)}");

        if (bmi < 18.5)
            Console.WriteLine("Underweight");
        else if (bmi < 25)
            Console.WriteLine("Normal");
        else if (bmi < 30)
            Console.WriteLine("Overweight");
        else
            Console.WriteLine("Obese");
    }
}