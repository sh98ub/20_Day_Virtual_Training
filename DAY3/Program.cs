//Payment System
using System;

abstract class Payment
{
    public decimal Amount { get; set; }

    public void ProcessPayment()
    {
        Validate();
        Pay();
        Console.WriteLine("Payment Successful");
    }

    protected virtual void Validate()
    {
        Console.WriteLine("Validation Completed");
    }

    protected abstract void Pay();
}

class CreditCardPayment : Payment
{
    protected override void Pay()
    {
        Console.WriteLine($"Paid {Amount} using Credit Card");
    }
}

class UPIPayment : Payment
{
    protected override void Pay()
    {
        Console.WriteLine($"Paid {Amount} using UPI");
    }
}

class NetBankingPayment : Payment
{
    protected override void Pay()
    {
        Console.WriteLine($"Paid {Amount} using Net Banking");
    }
}

class Program
{
    static void Main()
    {
        Payment payment = new UPIPayment { Amount = 2000 };
        payment.ProcessPayment();
    }
}

// Notification System

interface IMessageService
{
    void Send(string message);
}

class EmailService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine("Email : " + message);
    }
}

class SmsService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine("SMS : " + message);
    }
}

class Notification
{
    private IMessageService service;

    public Notification(IMessageService service)
    {
        this.service = service;
    }

    public void Notify(string msg)
    {
        service.Send(msg);
    }
}

class Program
{
    static void Main()
    {
        Notification n = new Notification(new EmailService());
        n.Notify("Welcome");
    }
}

// Discount System

using System;

abstract class Discount
{
    public abstract double Calculate(double amount);
}

class FestivalDiscount : Discount
{
    public override double Calculate(double amount)
    {
        return amount * 0.9;
    }
}

class PremiumDiscount : Discount
{
    public override double Calculate(double amount)
    {
        return amount * 0.8;
    }
}

class Program
{
    static void Main()
    {
        Discount d = new PremiumDiscount();
        Console.WriteLine(d.Calculate(1000));
    }
}

// Employee Encapsulation
using System;

class Employee
{
    public string Name { get; set; }

    private decimal salary;

    public decimal Salary
    {
        get { return salary; }
    }

    public void IncreaseSalary(decimal amount)
    {
        if (amount > 0)
            salary += amount;
    }
}

class Program
{
    static void Main()
    {
        Employee emp = new Employee();
        emp.IncreaseSalary(5000);
        Console.WriteLine(emp.Salary);
    }
}

//Open Closed Principle
using System;

abstract class PaymentMethod
{
    public abstract void Pay();
}

class Card : PaymentMethod
{
    public override void Pay()
    {
        Console.WriteLine("Card Payment");
    }
}

class Wallet : PaymentMethod
{
    public override void Pay()
    {
        Console.WriteLine("Wallet Payment");
    }
}

class Crypto : PaymentMethod
{
    public override void Pay()
    {
        Console.WriteLine("Crypto Payment");
    }
}

class Program
{
    static void Main()
    {
        PaymentMethod p = new Crypto();
        p.Pay();
    }
}