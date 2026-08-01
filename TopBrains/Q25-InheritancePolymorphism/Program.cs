using System;
using System.Collections.Generic;
using System.Globalization;

namespace Q25_InheritancePolymorphism
{
    public abstract class Employee
    {
        public abstract decimal CalculatePay();
    }

    public class HourlyEmployee : Employee
    {
        public decimal Rate { get; set; }
        public decimal Hours { get; set; }

        public HourlyEmployee(decimal rate, decimal hours)
        {
            Rate = rate;
            Hours = hours;
        }

        public override decimal CalculatePay()
        {
            return Rate * Hours;
        }
    }

    public class SalariedEmployee : Employee
    {
        public decimal MonthlySalary { get; set; }

        public SalariedEmployee(decimal monthlySalary)
        {
            MonthlySalary = monthlySalary;
        }

        public override decimal CalculatePay()
        {
            return MonthlySalary;
        }
    }

    public class CommissionEmployee : Employee
    {
        public decimal Commission { get; set; }
        public decimal BaseSalary { get; set; }

        public CommissionEmployee(decimal commission, decimal baseSalary)
        {
            Commission = commission;
            BaseSalary = baseSalary;
        }

        public override decimal CalculatePay()
        {
            return BaseSalary + Commission;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] employeesInput = {
                "H 15.50 40",
                "S 5000",
                "C 1200 3000"
            };

            decimal totalPayroll = CalculateTotalPayroll(employeesInput);
            Console.WriteLine($"Total Payroll: {totalPayroll:F2}");
        }

        public static decimal CalculateTotalPayroll(string[] employees)
        {
            if (employees == null || employees.Length == 0)
                return 0.00m;

            List<Employee> employeeList = new List<Employee>();

            foreach (var empStr in employees)
            {
                if (string.IsNullOrWhiteSpace(empStr)) continue;

                string[] parts = empStr.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;

                string type = parts[0].ToUpper();

                switch (type)
                {
                    case "H":
                        if (parts.Length >= 3 &&
                            decimal.TryParse(parts[1], CultureInfo.InvariantCulture, out decimal rate) &&
                            decimal.TryParse(parts[2], CultureInfo.InvariantCulture, out decimal hours))
                        {
                            employeeList.Add(new HourlyEmployee(rate, hours));
                        }
                        break;

                    case "S":
                        if (decimal.TryParse(parts[1], CultureInfo.InvariantCulture, out decimal salary))
                        {
                            employeeList.Add(new SalariedEmployee(salary));
                        }
                        break;

                    case "C":
                        if (parts.Length >= 3 &&
                            decimal.TryParse(parts[1], CultureInfo.InvariantCulture, out decimal comm) &&
                            decimal.TryParse(parts[2], CultureInfo.InvariantCulture, out decimal baseSal))
                        {
                            employeeList.Add(new CommissionEmployee(comm, baseSal));
                        }
                        break;
                }
            }

            decimal total = 0.00m;
            foreach (var emp in employeeList)
            {
                total += emp.CalculatePay();
            }

            return Math.Round(total, 2);
        }
    }
}
