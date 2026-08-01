using System;

namespace BankSys
{
    public class Account
    {
        // Private fields
        private string name;
        private double balance;

        // Constructor
        public Account(string name, double initialBalance)
        {
            this.name = name;
            this.balance = initialBalance;
        }

        // Deposit method
        public double deposit(double amount)
        {
            balance += amount;
            return balance;
        }

        // Get balance
        public double getBalance()
        {
            return balance;
        }

        // Set name
        public void setName(string newName)
        {
            name = newName;
        }

        // Get name
        public string getName()
        {
            return name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Account account1 = new Account("Alok Mittal", 1250.00);

            Console.WriteLine(account1.getBalance());   // 1250

            account1.setName("John Doe");
            Console.WriteLine(account1.getName());      // John Doe

            Account account2 = new Account("Amit", 500);
            Console.WriteLine(account2.getBalance());   // 500

            Console.WriteLine(account1.deposit(0.5));   // 1250.5
            Console.WriteLine(account1.getBalance());   // 1250.5

            account1.setName("Riya Amit Mehta");
            Console.WriteLine(account1.getName());      // Riya Amit Mehta
        }
    }
}
