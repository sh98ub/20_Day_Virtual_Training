using System;
using System.Collections.Generic;

namespace Q24_BikeRental
{
    public class Bike
    {
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public int PricePerDay { get; set; }
    }

    public class BikeUtility
    {
        public void AddBikeDetails(string model, string brand, int pricePerDay)
        {
            int nextKey = Program.bikeDetails.Count + 1;
            Bike bike = new Bike
            {
                Model = model,
                Brand = brand,
                PricePerDay = pricePerDay
            };
            Program.bikeDetails.Add(nextKey, bike);
        }

        public SortedDictionary<string, List<Bike>> GroupBikesByBrand()
        {
            SortedDictionary<string, List<Bike>> grouped = new SortedDictionary<string, List<Bike>>();

            foreach (var entry in Program.bikeDetails)
            {
                Bike bike = entry.Value;
                if (!grouped.ContainsKey(bike.Brand))
                {
                    grouped[bike.Brand] = new List<Bike>();
                }
                grouped[bike.Brand].Add(bike);
            }

            return grouped;
        }
    }

    public class Program
    {
        public static SortedDictionary<int, Bike> bikeDetails = new SortedDictionary<int, Bike>();

        public static void Main(string[] args)
        {
            BikeUtility utility = new BikeUtility();

            while (true)
            {
                Console.WriteLine("\n 1. Add Bike Details");
                Console.WriteLine(" 2. Group Bikes By Brand");
                Console.WriteLine(" 3. Exit\n");
                Console.Write(" Enter your choice: ");

                string choice = Console.ReadLine()!.Trim();

                if (choice == "1")
                {
                    Console.Write("\n Enter the model: ");
                    string model = Console.ReadLine()!;

                    Console.Write(" Enter the brand: ");
                    string brand = Console.ReadLine()!;

                    Console.Write(" Enter the price per day: ");
                    int price = int.Parse(Console.ReadLine()!);

                    utility.AddBikeDetails(model, brand, price);
                    Console.WriteLine("\n Bike details added successfully");
                }
                else if (choice == "2")
                {
                    var grouped = utility.GroupBikesByBrand();
                    Console.WriteLine();
                    foreach (var kvp in grouped)
                    {
                        foreach (var bike in kvp.Value)
                        {
                            Console.WriteLine($" {bike.Brand} {bike.Model}");
                        }
                    }
                }
                else if (choice == "3")
                {
                    break;
                }
            }
        }
    }
}
