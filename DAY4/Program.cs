// question 1
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliverySystem
{
    public abstract class FoodItem
    {
        private string name;
        private double price;

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                    price = value;
            }
        }

        public abstract double CalculatePrice();

        public abstract void PrepareFood();
    }

    public interface IDiscount
    {
        double ApplyDiscount();
    }

    public class Pizza : FoodItem, IDiscount
    {
        public double Discount { get; set; }

        public override double CalculatePrice()
        {
            return Price;
        }

        public double ApplyDiscount()
        {
            return CalculatePrice() - Discount;
        }

        public override void PrepareFood()
        {
            Console.WriteLine("Preparing Pizza...");
        }
    }

    public class Burger : FoodItem, IDiscount
    {
        public double Discount { get; set; }

        public override double CalculatePrice()
        {
            return Price;
        }

        public double ApplyDiscount()
        {
            return CalculatePrice() - Discount;
        }

        public override void PrepareFood()
        {
            Console.WriteLine("Preparing Burger...");
        }
    }

    public class Sandwich : FoodItem, IDiscount
    {
        public double Discount { get; set; }

        public override double CalculatePrice()
        {
            return Price;
        }

        public double ApplyDiscount()
        {
            return CalculatePrice() - Discount;
        }

        public override void PrepareFood()
        {
            Console.WriteLine("Preparing Sandwich...");
        }
    }

    public class Order<T> where T : FoodItem
    {
        private List<T> items = new List<T>();

        public void AddItem(T item)
        {
            items.Add(item);
        }

        public List<T> Items
        {
            get { return items; }
        }

        public T this[int index]
        {
            get { return items[index]; }
        }
    }

    public static class OrderExtensions
    {
        public static double GetTotalBill<T>(this Order<T> order) where T : FoodItem
        {
            return order.Items.Sum(item => item.CalculatePrice());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Order<FoodItem> order = new Order<FoodItem>();

            Pizza pizza = new Pizza
            {
                Name = "Veg Pizza",
                Price = 500,
                Discount = 50
            };

            Burger burger = new Burger
            {
                Name = "Cheese Burger",
                Price = 250,
                Discount = 20
            };

            Sandwich sandwich = new Sandwich
            {
                Name = "Grilled Sandwich",
                Price = 180,
                Discount = 10
            };

            order.AddItem(pizza);
            order.AddItem(burger);
            order.AddItem(sandwich);

            Console.WriteLine("Food Items");

            foreach (FoodItem item in order.Items)
            {
                item.PrepareFood();
                Console.WriteLine("Name : " + item.Name);
                Console.WriteLine("Price : " + item.CalculatePrice());

                if (item is IDiscount discount)
                {
                    Console.WriteLine("Price After Discount : " + discount.ApplyDiscount());
                }

                Console.WriteLine();
            }

            Console.WriteLine("First Item Using Indexer");
            Console.WriteLine(order[0].Name);

            Console.WriteLine();

            Console.WriteLine("Total Bill : " + order.GetTotalBill());

            Console.WriteLine();

            var invoice = new
            {
                ItemCount = order.Items.Count,
                TotalAmount = order.GetTotalBill(),
                FoodItems = order.Items.Select(x => x.Name).ToList()
            };

            Console.WriteLine("Invoice Summary");
            Console.WriteLine("Items : " + invoice.ItemCount);
            Console.WriteLine("Total : " + invoice.TotalAmount);

            foreach (var item in invoice.FoodItems)
            {
                Console.WriteLine(item);
            }
        }
    }
}

//ques 2

using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericCacheManager
{
    class InvalidCacheKeyException : Exception
    {
        public InvalidCacheKeyException(string message) : base(message)
        {
        }
    }

    class CacheItem<T>
    {
        public T Value { get; set; }
        public DateTime ExpiryTime { get; set; }
    }

    class CacheManager<T>
    {
        private Dictionary<string, CacheItem<T>> cache = new Dictionary<string, CacheItem<T>>();

        public void Add(string key, T value, DateTime expiry)
        {
            cache[key] = new CacheItem<T>
            {
                Value = value,
                ExpiryTime = expiry
            };
        }

        public void Remove(string key)
        {
            if (!cache.ContainsKey(key))
                throw new InvalidCacheKeyException("Key not found.");

            cache.Remove(key);
        }

        public T GetByKey(string key)
        {
            if (!cache.ContainsKey(key))
                throw new InvalidCacheKeyException("Key not found.");

            return cache[key].Value;
        }

        public void Clear()
        {
            cache.Clear();
        }

        public Dictionary<string, CacheItem<T>> Cache
        {
            get { return cache; }
        }

        public T this[string key]
        {
            get
            {
                if (!cache.ContainsKey(key))
                    throw new InvalidCacheKeyException("Key not found.");

                return cache[key].Value;
            }
        }
    }

    static class CacheExtensions
    {
        public static List<string> GetAllKeys<T>(this CacheManager<T> manager)
        {
            return manager.Cache.Keys.ToList();
        }

        public static int CountExpiredItems<T>(this CacheManager<T> manager)
        {
            return manager.Cache.Values.Count(x => x.ExpiryTime < DateTime.Now);
        }
    }

    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Order
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CacheManager<int> intCache = new CacheManager<int>();

            intCache.Add("A", 100, DateTime.Now.AddMinutes(5));
            intCache.Add("B", 200, DateTime.Now.AddMinutes(-5));

            Console.WriteLine("Integer Cache");
            Console.WriteLine(intCache["A"]);

            Console.WriteLine();

            CacheManager<Customer> customerCache = new CacheManager<Customer>();

            customerCache.Add("C1",
                new Customer
                {
                    Id = 1,
                    Name = "John"
                },
                DateTime.Now.AddMinutes(10));

            Console.WriteLine("Customer Cache");
            Console.WriteLine(customerCache["C1"].Name);

            Console.WriteLine();

            CacheManager<Order> orderCache = new CacheManager<Order>();

            orderCache.Add("O1",
                new Order
                {
                    OrderId = 101,
                    Amount = 2500
                },
                DateTime.Now.AddMinutes(2));

            Console.WriteLine("Order Cache");
            Console.WriteLine(orderCache["O1"].Amount);

            Console.WriteLine();

            Console.WriteLine("All Keys");
            foreach (var key in intCache.GetAllKeys())
            {
                Console.WriteLine(key);
            }

            Console.WriteLine();

            Console.WriteLine("Expired Items");
            Console.WriteLine(intCache.CountExpiredItems());

            Console.WriteLine();

            Console.WriteLine("Get By Key");
            Console.WriteLine(intCache.GetByKey("A"));

            Console.WriteLine();

            intCache.Remove("A");

            Console.WriteLine("Keys After Remove");
            foreach (var key in intCache.GetAllKeys())
            {
                Console.WriteLine(key);
            }

            Console.WriteLine();

            intCache.Clear();

            Console.WriteLine("Cache Cleared");
            Console.WriteLine(intCache.GetAllKeys().Count);
        }
    }
}


//question 3
using System;
using System.Collections.Generic;

namespace NotificationFramework
{
    public interface INotification
    {
        string Status { get; set; }
        void Send(string message);
    }

    public class Email : INotification
    {
        public string Status { get; set; }

        public void Send(string message)
        {
            Console.WriteLine("Email Sent : " + message);
            Status = "Success";
        }
    }

    public class SMS : INotification
    {
        public string Status { get; set; }

        public void Send(string message)
        {
            Console.WriteLine("SMS Sent : " + message);
            Status = "Success";
        }
    }

    public class WhatsApp : INotification
    {
        public string Status { get; set; }

        public void Send(string message)
        {
            Console.WriteLine("WhatsApp Sent : " + message);
            Status = "Success";
        }
    }

    public class PushNotification : INotification
    {
        public string Status { get; set; }

        public void Send(string message)
        {
            Console.WriteLine("Push Notification Sent : " + message);
            Status = "Success";
        }
    }

    public class NotificationManager
    {
        private List<INotification> notifications = new List<INotification>();

        public void AddNotification(INotification notification)
        {
            notifications.Add(notification);
        }

        public void SendAll(string message)
        {
            foreach (var notification in notifications)
            {
                notification.Send(message);
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine();
            Console.WriteLine("Notification Status");

            foreach (var notification in notifications)
            {
                Console.WriteLine(notification.GetType().Name + " : " + notification.Status);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            NotificationManager manager = new NotificationManager();

            manager.AddNotification(new Email());
            manager.AddNotification(new SMS());
            manager.AddNotification(new WhatsApp());
            manager.AddNotification(new PushNotification());

            manager.SendAll("Your order has been delivered.");

            manager.DisplayStatus();
        }
    }
}

//quetion 4
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollEngine
{
    abstract class Employee
    {
        private int id;
        private string name;

        public int Id
        {
            get { return id; }
            set
            {
                if (value > 0)
                    id = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
            }
        }

        public double BasicSalary { get; set; }

        public abstract double CalculateSalary();

        public abstract double CalculateBonus();
    }

    class PermanentEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BasicSalary + (BasicSalary * 0.20);
        }

        public override double CalculateBonus()
        {
            return BasicSalary * 0.10;
        }
    }

    class ContractEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BasicSalary;
        }

        public override double CalculateBonus()
        {
            return BasicSalary * 0.05;
        }
    }

    class Intern : Employee
    {
        public override double CalculateSalary()
        {
            return BasicSalary;
        }

        public override double CalculateBonus()
        {
            return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>()
            {
                new PermanentEmployee
                {
                    Id = 1,
                    Name = "Pankaj",
                    BasicSalary = 50000
                },

                new ContractEmployee
                {
                    Id = 2,
                    Name = "Rahul",
                    BasicSalary = 30000
                },

                new Intern
                {
                    Id = 3,
                    Name = "Amit",
                    BasicSalary = 15000
                }
            };

            Console.WriteLine("Employee Payroll Report\n");

            foreach (Employee emp in employees)
            {
                Console.WriteLine("Id : " + emp.Id);
                Console.WriteLine("Name : " + emp.Name);
                Console.WriteLine("Salary : " + emp.CalculateSalary());
                Console.WriteLine("Bonus : " + emp.CalculateBonus());
                Console.WriteLine();
            }

            var report = employees.Select(emp => new
            {
                emp.Id,
                emp.Name,
                Salary = emp.CalculateSalary(),
                Bonus = emp.CalculateBonus()
            });

            Console.WriteLine("Anonymous Payroll Report\n");

            foreach (var item in report)
            {
                Console.WriteLine($"Id : {item.Id}");
                Console.WriteLine($"Name : {item.Name}");
                Console.WriteLine($"Salary : {item.Salary}");
                Console.WriteLine($"Bonus : {item.Bonus}");
                Console.WriteLine();
            }
        }
    }
}

//question 5
