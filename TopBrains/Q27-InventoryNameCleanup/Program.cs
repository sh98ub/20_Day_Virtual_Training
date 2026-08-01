using System;
using System.Globalization;
using System.Text;

namespace Q27_InventoryNameCleanup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = " llapppptop bag ";
            string output = CleanupInventoryName(input);

            Console.WriteLine($"Input:  \"{input}\"");
            Console.WriteLine($"Output: \"{output}\"");
        }

        public static string CleanupInventoryName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";

            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (sb.Length == 0 || char.ToLower(c) != char.ToLower(sb[sb.Length - 1]))
                {
                    sb.Append(c);
                }
            }

            string trimmed = sb.ToString().Trim();
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(trimmed.ToLower());
        }
    }
}
