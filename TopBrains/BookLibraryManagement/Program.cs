using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BookLibraryManagement
{
    internal class Program
    {
        private static List<dynamic> books = new List<dynamic>();
        private static int nextId = 1;

        static void Main(string[] args)
        {
            SeedData();

            if (args.Length > 0 && args[0] == "--test")
            {
                RunAutomatedTests();
                return;
            }

            while (true)
            {
                Console.WriteLine("\n=== Book Library Management System ===");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. User Menu");
                Console.WriteLine("3. Run Test Cases Demonstration");
                Console.WriteLine("4. Exit");
                Console.Write("Select Role/Option: ");

                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        AdminMenu();
                        break;
                    case "2":
                        UserMenu();
                        break;
                    case "3":
                        RunAutomatedTests();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void SeedData()
        {
            AddBookInternal("C# in Depth", "Jon Skeet", "Manning", 45.99);
            AddBookInternal("Clean Code", "Robert C. Martin", "Prentice Hall", 39.95);
            AddBookInternal("The Pragmatic Programmer", "Andrew Hunt", "Addison-Wesley", 49.99);
            AddBookInternal("Design Patterns", "Erich Gamma", "Addison-Wesley", 54.50);
            AddBookInternal("Head First Design Patterns", "Eric Freeman", "O'Reilly", 29.99);
        }

        static dynamic CreateBook(int id, string title, string author, string publisher, double price)
        {
            dynamic book = new ExpandoObject();
            book.Id = id;
            book.Title = title;
            book.Author = author;
            book.Publisher = publisher;
            book.Price = price;
            return book;
        }

        static void AddBookInternal(string title, string author, string publisher, double price)
        {
            dynamic book = CreateBook(nextId++, title, author, publisher, price);
            books.Add(book);
        }

        // Admin Operations

        static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Update Book");
                Console.WriteLine("3. Delete Book");
                Console.WriteLine("4. View All Books");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Choice: ");

                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        AddBookUI();
                        break;
                    case "2":
                        UpdateBookUI();
                        break;
                    case "3":
                        DeleteBookUI();
                        break;
                    case "4":
                        DisplayBooks(books);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddBookUI()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine()!;
            Console.Write("Enter Author: ");
            string author = Console.ReadLine()!;
            Console.Write("Enter Publisher: ");
            string publisher = Console.ReadLine()!;
            Console.Write("Enter Price: ");
            if (double.TryParse(Console.ReadLine(), out double price))
            {
                AddBookInternal(title, author, publisher, price);
                Console.WriteLine("Book added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid price input.");
            }
        }

        static void UpdateBookUI()
        {
            Console.Write("Enter Book ID to Update: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            dynamic? book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.Write($"Enter New Title ({book.Title}): ");
            string title = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(title)) book.Title = title;

            Console.Write($"Enter New Author ({book.Author}): ");
            string author = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(author)) book.Author = author;

            Console.Write($"Enter New Publisher ({book.Publisher}): ");
            string publisher = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(publisher)) book.Publisher = publisher;

            Console.Write($"Enter New Price ({book.Price}): ");
            string priceStr = Console.ReadLine()!;
            if (double.TryParse(priceStr, out double price)) book.Price = price;

            Console.WriteLine("Book updated successfully.");
        }

        static void DeleteBookUI()
        {
            Console.Write("Enter Book ID to Delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            dynamic? book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine("Book deleted successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // User Operations

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- User Menu ---");
                Console.WriteLine("1. Browse All Books");
                Console.WriteLine("2. Search Book by Name");
                Console.WriteLine("3. Search Book by Publisher");
                Console.WriteLine("4. View Highest Price Book");
                Console.WriteLine("5. View Lowest Price Book");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choice: ");

                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        DisplayBooks(books);
                        break;
                    case "2":
                        SearchByNameUI();
                        break;
                    case "3":
                        SearchByPublisherUI();
                        break;
                    case "4":
                        ViewHighestPriceBook();
                        break;
                    case "5":
                        ViewLowestPriceBook();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void SearchByNameUI()
        {
            Console.Write("Enter Book Name: ");
            string query = Console.ReadLine()!;
            var results = books.Where(b => ((string)b.Title).Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            DisplayBooks(results);
        }

        static void SearchByPublisherUI()
        {
            Console.Write("Enter Publisher Name: ");
            string query = Console.ReadLine()!;
            var results = books.Where(b => ((string)b.Publisher).Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            DisplayBooks(results);
        }

        static void ViewHighestPriceBook()
        {
            if (!books.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }
            double maxPrice = books.Max(b => (double)b.Price);
            var highest = books.Where(b => (double)b.Price == maxPrice).ToList();
            Console.WriteLine("\nHighest Price Book(s):");
            DisplayBooks(highest);
        }

        static void ViewLowestPriceBook()
        {
            if (!books.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }
            double minPrice = books.Min(b => (double)b.Price);
            var lowest = books.Where(b => (double)b.Price == minPrice).ToList();
            Console.WriteLine("\nLowest Price Book(s):");
            DisplayBooks(lowest);
        }

        static void DisplayBooks(IEnumerable<dynamic> bookList)
        {
            if (!bookList.Any())
            {
                Console.WriteLine("No books found.");
                return;
            }

            Console.WriteLine("\n----------------------------------------------------------------------------------");
            Console.WriteLine($"{"ID",-5} | {"Title",-30} | {"Author",-20} | {"Publisher",-16} | {"Price",-8}");
            Console.WriteLine("----------------------------------------------------------------------------------");
            foreach (var b in bookList)
            {
                Console.WriteLine($"{b.Id,-5} | {b.Title,-30} | {b.Author,-20} | {b.Publisher,-16} | ${b.Price,-8:F2}");
            }
            Console.WriteLine("----------------------------------------------------------------------------------");
        }

        // Automated Test Cases Execution

        static void RunAutomatedTests()
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine("     RUNNING AUTOMATED TEST CASES");
            Console.WriteLine("===========================================");

            // Test Case 1: Add Book
            Console.WriteLine("\n[Test Case 1: Add Book]");
            AddBookInternal("Refactoring", "Martin Fowler", "Addison-Wesley", 47.50);
            Console.WriteLine("Expected: Book added to list.");
            Console.WriteLine($"Actual: Book added with ID {nextId - 1}. Total books: {books.Count}");

            // Test Case 2: Update Book
            Console.WriteLine("\n[Test Case 2: Update Book]");
            dynamic? bToUpdate = books.FirstOrDefault(b => b.Id == 1);
            if (bToUpdate != null)
            {
                bToUpdate.Price = 42.00;
                Console.WriteLine($"Updated Book ID 1 Price to ${bToUpdate.Price:F2}");
            }

            // Test Case 3: Delete Book
            Console.WriteLine("\n[Test Case 3: Delete Book]");
            dynamic? bToDelete = books.FirstOrDefault(b => b.Id == 2);
            if (bToDelete != null)
            {
                books.Remove(bToDelete);
                Console.WriteLine("Deleted Book ID 2 (Clean Code).");
            }

            // Test Case 4: Search by Name
            Console.WriteLine("\n[Test Case 4: Search by Name ('Refactoring')]");
            var nameSearch = books.Where(b => ((string)b.Title).Contains("Refactoring", StringComparison.OrdinalIgnoreCase)).ToList();
            DisplayBooks(nameSearch);

            // Test Case 5: Search by Publisher ('Addison-Wesley')
            Console.WriteLine("\n[Test Case 5: Search by Publisher ('Addison-Wesley')]");
            var pubSearch = books.Where(b => ((string)b.Publisher).Contains("Addison-Wesley", StringComparison.OrdinalIgnoreCase)).ToList();
            DisplayBooks(pubSearch);

            // Test Case 6: Highest Price Book
            Console.WriteLine("\n[Test Case 6: Highest Price Book]");
            ViewHighestPriceBook();

            // Test Case 7: Lowest Price Book
            Console.WriteLine("\n[Test Case 7: Lowest Price Book]");
            ViewLowestPriceBook();

            Console.WriteLine("\n===========================================");
            Console.WriteLine("     ALL TEST CASES EXECUTED SUCCESSFULLY");
            Console.WriteLine("===========================================\n");
        }
    }
}
