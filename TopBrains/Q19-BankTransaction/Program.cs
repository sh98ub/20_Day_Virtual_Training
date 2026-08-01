using System;

namespace Q19_BankTransaction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long initialBalance = 1000;
            int[] transactions = { 500, -200, -1500, -400, 300 };

            long finalBalance = CalculateFinalBalance(initialBalance, transactions);

            Console.WriteLine($"Initial Balance: {initialBalance}");
            Console.WriteLine($"Transactions: [{string.Join(", ", transactions)}]");
            Console.WriteLine($"Final Balance: {finalBalance}");
        }

        public static long CalculateFinalBalance(long initialBalance, int[] transactions)
        {
            long balance = initialBalance;

            foreach (int tx in transactions)
            {
                if (tx >= 0)
                {
                    balance += tx;
                }
                else if (balance + tx >= 0)
                {
                    balance += tx;
                }
            }

            return balance;
        }
    }
}
