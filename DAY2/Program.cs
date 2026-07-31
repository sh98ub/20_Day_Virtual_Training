// /QUESTION M1

namespace M1
{
    /// <summary>
    /// Performs financial calculations.
    /// </summary>
    public static class FinancialCalculator
    {
        /// <summary>
        /// Calculates compound interest using annual compounding.
        /// </summary>
        public static double CalculateCompoundInterest(double principal, double rate, int time)
        {
            return CalculateCompoundInterest(principal, rate, time, 1);
        }

        /// <summary>
        /// Calculates compound interest with specified compounding frequency.
        /// </summary>
        public static double CalculateCompoundInterest(
            double principal,
            double rate,
            int time = 1,
            int compoundingFrequency = 1)
        {
            return principal * Math.Pow((1 + rate / compoundingFrequency),
                compoundingFrequency * time);
        }
    }

    class Program
    {
        static void Main()
        {
            double amount1 = FinancialCalculator.CalculateCompoundInterest(10000, 0.05, 10);

            double amount2 = FinancialCalculator.CalculateCompoundInterest(
                principal: 10000,
                rate: 0.05,
                time: 10,
                compoundingFrequency: 12);

            Console.WriteLine($"Annual : {amount1:F2}");
            Console.WriteLine($"Monthly: {amount2:F2}");
        }
    }
}

//QUESTION M2


namespace M2
{
    public static class LibraryProcessor
    {
        public static bool TryParseISBN(string input, out string cleanedISBN)
        {
            cleanedISBN = input.Replace("-", "").Trim();

            if (cleanedISBN.Length == 13)
                return true;

            cleanedISBN = "";
            return false;
        }

        public static bool TryProcessOrder(out List<string> validISBNs, params string[] isbnList)
        {
            validISBNs = new List<string>();

            foreach (string item in isbnList)
            {
                string[] books = item.Split(',');

                foreach (string isbn in books)
                {
                    if (TryParseISBN(isbn, out string cleaned))
                        validISBNs.Add(cleaned);
                }
            }

            return validISBNs.Count > 0;
        }
    }

    class Program
    {
        static void Main()
        {
            bool result = LibraryProcessor.TryProcessOrder(
                out List<string> books,
                "978-3-16-148410-0,1234567890123,invalid-isbn,978-1-4028-9462-6");

            Console.WriteLine(result);

            foreach (string item in books)
                Console.WriteLine(item);
        }
    }
}

//QUESTION M3

namespace M3
{
    enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public static class LogParser
    {
        public static bool ParseLogLine(
            in string line,
            out DateTime timestamp,
            out LogLevel level,
            ref int counter)
        {
            timestamp = DateTime.MinValue;
            level = LogLevel.Info;

            counter++;

            string[] parts = line.Split(' ', 3);

            if (!DateTime.TryParse(parts[0] + " " + parts[1], out timestamp))
                return false;

            if (line.Contains("ERROR"))
                level = LogLevel.Error;
            else if (line.Contains("WARNING"))
                level = LogLevel.Warning;

            return true;
        }
    }

    class Program
    {
        static void Main()
        {
            int count = 0;

            string log = "2023-10-27 14:30:00 ERROR: Disk full";

            LogParser.ParseLogLine(
                in log,
                out DateTime time,
                out LogLevel level,
                ref count);

            Console.WriteLine(time);
            Console.WriteLine(level);
            Console.WriteLine(count);
        }
    }
}

//QUESTION M4


namespace M4
{
    public static class Geometry
    {
        public static double CalculateArea(double radius, int decimals = 2)
        {
            return Math.Round(Math.PI * radius * radius, decimals);
        }

        public static int CalculateArea(int length, int breadth)
        {
            return length * breadth;
        }

        public static double CalculateArea(int b, int h, bool triangle)
        {
            return 0.5 * b * h;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine(Geometry.CalculateArea(5));

            Console.WriteLine(Geometry.CalculateArea(4, 6));

            Console.WriteLine(Geometry.CalculateArea(3, 7, true));

            Console.WriteLine(
                Geometry.CalculateArea(radius: 5, decimals: 4));
        }
    }
}

// QUESTION M5


namespace M5
{
    public static class MathOperations
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static int Add(params int[] numbers)
        {
            int sum = 0;

            foreach (int n in numbers)
                sum += n;

            return sum;
        }

        public static int Multiply(int a, int b)
        {
            return a * b;
        }

        public static int Multiply(params int[] numbers)
        {
            int product = 1;

            foreach (int n in numbers)
                product *= n;

            return product;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine(MathOperations.Add(5, 10));

            Console.WriteLine(MathOperations.Add(1, 2, 3, 4, 5));

            Console.WriteLine(MathOperations.Multiply(2, 3));

            Console.WriteLine(MathOperations.Multiply(2, 3, 4, 5));
        }
    }
}

//Question 6 (H1)


namespace ConfigurationDemo
{
    public class Configuration
    {
        public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
    }

    public interface IConfigurationSource
    {
        bool TryLoad(out Configuration config);
    }

    public class EnvironmentVariableSource : IConfigurationSource
    {
        public bool TryLoad(out Configuration config)
        {
            config = null;
            Console.WriteLine("Checking Environment Variables...");
            return false;
        }
    }

    public class JsonFileSource : IConfigurationSource
    {
        private readonly string fileName;

        public JsonFileSource(string fileName)
        {
            this.fileName = fileName;
        }

        public bool TryLoad(out Configuration config)
        {
            config = null;
            Console.WriteLine($"Checking JSON File ({fileName})...");
            return false;
        }
    }

    public class DatabaseSource : IConfigurationSource
    {
        public bool TryLoad(out Configuration config)
        {
            Console.WriteLine("Checking Database...");

            config = new Configuration();
            config.Settings["Server"] = "SQL01";
            config.Settings["Database"] = "Production";
            config.Settings["Timeout"] = "30";

            return true;
        }
    }

    public static class ConfigurationLoader
    {
        public static bool Load(out Configuration configuration, params IConfigurationSource[] sources)
        {
            configuration = null;

            foreach (IConfigurationSource source in sources)
            {
                if (source.TryLoad(out configuration))
                {
                    Console.WriteLine($"Configuration loaded successfully from {source.GetType().Name}");
                    return true;
                }

                Console.WriteLine($"{source.GetType().Name} failed.\n");
            }

            return false;
        }
    }

    class Program
    {
        static void Main()
        {
            bool result = ConfigurationLoader.Load(
                out Configuration config,
                new EnvironmentVariableSource(),
                new JsonFileSource("config.json"),
                new DatabaseSource());

            Console.WriteLine();

            if (result)
            {
                Console.WriteLine("Configuration Values:");

                foreach (var item in config.Settings)
                {
                    Console.WriteLine($"{item.Key} = {item.Value}");
                }
            }
            else
            {
                Console.WriteLine("No configuration source succeeded.");
            }
        }
    }
}
//Question 7 (H2)


namespace FlattenTreeDemo
{
    public class TreeNode
    {
        public string Value { get; set; }
        public List<TreeNode> Children { get; set; }

        public TreeNode(string value)
        {
            Value = value;
            Children = new List<TreeNode>();
        }
    }

    public static class TreeHelper
    {
        public static List<string> FlattenTree(params TreeNode[] roots)
        {
            List<string> result = new List<string>();

            foreach (TreeNode root in roots)
            {
                int depth = 0;
                Traverse(root, ref depth);
            }

            return result;

            void Traverse(TreeNode node, ref int depth)
            {
                if (node == null)
                    return;

                Console.WriteLine($"{node.Value} : Depth = {depth}");
                result.Add(node.Value);

                foreach (TreeNode child in node.Children)
                {
                    depth++;
                    Traverse(child, ref depth);
                    depth--;
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            TreeNode root1 = new TreeNode("A");
            root1.Children.Add(new TreeNode("A1"));
            root1.Children.Add(new TreeNode("A2"));

            TreeNode root2 = new TreeNode("B");
            TreeNode b1 = new TreeNode("B1");
            b1.Children.Add(new TreeNode("B1a"));
            b1.Children.Add(new TreeNode("B1b"));
            root2.Children.Add(b1);

            TreeNode root3 = new TreeNode("C");

            List<string> flattened = TreeHelper.FlattenTree(root1, root2, root3);

            Console.WriteLine("\nFlattened Tree:");

            foreach (string item in flattened)
            {
                Console.Write(item + " ");
            }
        }
    }
}

// Question 8 (H3)


namespace LogFormatterDemo
{
    public static class Logger
    {
        public static string FormatLogMessage(string template, params object[] args)
        {
            string result = template;

            void ReplacePlaceholders()
            {
                ReadOnlySpan<char> span = result.AsSpan();

                for (int i = 0; i < args.Length; i++)
                {
                    string placeholder = "{" + i + "}";
                    string value = args[i]?.ToString() ?? "";

                    if (args[i] is string str && int.TryParse(str, out int number))
                    {
                        value = number.ToString();
                    }
                    else if (args[i] is DateTime dt)
                    {
                        value = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    result = result.Replace(placeholder, value);
                }
            }

            ReplacePlaceholders();
            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            string message = Logger.FormatLogMessage(
                "User {0} logged in from {1} at {2}",
                "JohnDoe",
                "192.168.1.1",
                new DateTime(2026, 7, 29, 14, 30, 0));

            Console.WriteLine(message);
        }
    }
}
// Question 9 (H4)


namespace RiskAssessmentDemo
{
    public class Transaction
    {
        public string Id { get; set; }
        public List<Transaction> Dependencies { get; set; }

        public Transaction(string id)
        {
            Id = id;
            Dependencies = new List<Transaction>();
        }
    }

    public static class RiskCalculator
    {
        private const int MaxDepth = 1000;

        public static int CalculateRiskScore(
            string transactionId,
            Dictionary<string, Transaction> transactions)
        {
            if (!TryParseTransactionId(transactionId, out string id))
            {
                Console.WriteLine("Invalid Transaction ID");
                return -1;
            }

            if (!transactions.ContainsKey(id))
            {
                Console.WriteLine("Transaction not found");
                return -1;
            }

            int depth = 0;
            HashSet<string> visited = new HashSet<string>();

            return Calculate(transactions[id], ref depth, visited);
        }

        private static int Calculate(
            Transaction transaction,
            ref int depth,
            HashSet<string> visited)
        {
            if (depth > MaxDepth)
            {
                Console.WriteLine("Maximum recursion depth exceeded.");
                return -1;
            }

            if (visited.Contains(transaction.Id))
            {
                Console.WriteLine("Circular reference detected.");
                return 0;
            }

            visited.Add(transaction.Id);

            int score = 1;

            foreach (Transaction child in transaction.Dependencies)
            {
                depth++;

                int childScore = Calculate(child, ref depth, visited);

                if (childScore == -1)
                    return -1;

                score += childScore;

                depth--;
            }

            return score;
        }

        private static bool TryParseTransactionId(
            string input,
            out string transactionId)
        {
            transactionId = input.Trim();

            return transactionId.StartsWith("TX")
                   && transactionId.Length >= 5;
        }
    }

    class Program
    {
        static void Main()
        {
            Transaction t1 = new Transaction("TX001");
            Transaction t2 = new Transaction("TX002");
            Transaction t3 = new Transaction("TX003");

            t1.Dependencies.Add(t2);
            t2.Dependencies.Add(t3);
            t3.Dependencies.Add(t1);

            Dictionary<string, Transaction> transactions =
                new Dictionary<string, Transaction>();

            transactions.Add(t1.Id, t1);
            transactions.Add(t2.Id, t2);
            transactions.Add(t3.Id, t3);

            int score = RiskCalculator.CalculateRiskScore(
                "TX001",
                transactions);

            Console.WriteLine($"Risk Score = {score}");
        }
    }
}
//Question 10 (H5)

namespace QueryBuilderDemo
{
    public class QueryBuilder
    {
        private readonly StringBuilder sql = new StringBuilder();

        public void AddWhereClause(string clause)
        {
            if (sql.Length == 0)
                sql.AppendLine("WHERE " + clause);
            else
                sql.AppendLine("AND " + clause);
        }

        public void AddWhereClause(params Action<QueryBuilder>[] conditions)
        {
            int indent = 0;

            if (sql.Length == 0)
                sql.AppendLine("WHERE");
            else
                sql.AppendLine("AND");

            sql.AppendLine("(");

            ProcessConditions(ref indent);

            sql.AppendLine(")");

            void ProcessConditions(ref int level)
            {
                string space = new string(' ', (level + 1) * 4);

                for (int i = 0; i < conditions.Length; i++)
                {
                    QueryBuilder builder = new QueryBuilder();
                    conditions[i](builder);

                    string[] lines = builder.ToString()
                                            .Split(Environment.NewLine,
                                            StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in lines)
                    {
                        sql.AppendLine(space + line);
                    }

                    if (i < conditions.Length - 1)
                    {
                        sql.AppendLine(space + "OR");
                    }
                }
            }
        }

        public override string ToString()
        {
            return sql.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            QueryBuilder builder = new QueryBuilder();

            builder.AddWhereClause("Status = 'Active'");

            builder.AddWhereClause(

                b =>
                {
                    b.AddWhereClause("Age > 18");
                },

                b =>
                {
                    b.AddWhereClause("Age < 65");
                }

            );

            Console.WriteLine(builder);
        }
    }
}