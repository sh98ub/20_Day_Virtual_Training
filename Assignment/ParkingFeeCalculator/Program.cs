using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== PARKING FEE CALCULATOR ===");

        Console.Write("Enter Vehicle Type (C/M/T): ");
        char vehicleType = Char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        Console.Write("Enter Parking Hours: ");
        double hours = Convert.ToDouble(Console.ReadLine());

        double rate = GetHourlyRate(vehicleType);
        double dailyMax = GetDailyMaximum(vehicleType);

        double fee;

        // First 30 minutes free
        if (hours <= 0.5)
        {
            fee = 0;
        }
        else
        {
            fee = (hours - 0.5) * rate;

            // Apply daily maximum
            if (fee > dailyMax)
                fee = dailyMax;

            // Apply 10% discount for parking over 8 hours
            if (hours > 8)
                fee *= 0.90;
        }

        Console.WriteLine();
        Console.WriteLine("Vehicle: " + GetVehicleName(vehicleType));
        Console.WriteLine($"Parking Duration: {hours:F2} hours");
        Console.WriteLine($"Hourly Rate: ${rate:F2}");
        Console.WriteLine($"Daily Maximum: ${dailyMax:F2}");
        Console.WriteLine($"Total Fee: ${fee:F2}");
    }

    static double GetHourlyRate(char vehicleType)
    {
        switch (vehicleType)
        {
            case 'C':
                return 3.0;
            case 'M':
                return 2.0;
            case 'T':
                return 5.0;
            default:
                return 0.0;
        }
    }

    static double GetDailyMaximum(char vehicleType)
    {
        switch (vehicleType)
        {
            case 'C':
                return 25.0;
            case 'M':
                return 15.0;
            case 'T':
                return 40.0;
            default:
                return 0.0;
        }
    }

    static string GetVehicleName(char vehicleType)
    {
        switch (vehicleType)
        {
            case 'C':
                return "Car";
            case 'M':
                return "Motorcycle";
            case 'T':
                return "Truck";
            default:
                return "Unknown";
        }
    }
}
