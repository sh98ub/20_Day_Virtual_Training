using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Q4_StringFormat
{
    record Student(string Name, int Score);

    internal class Program
    {
        static void Main(string[] args)
        {
            string[] items = { "Alice:85", "Bob:70", "Charlie:85", "David:60", "Eve:90" };
            int minScore = 70;

            string jsonResult = FilterAndSerializeStudents(items, minScore);
            Console.WriteLine(jsonResult);
        }

        static string FilterAndSerializeStudents(string[] items, int minScore)
        {
            var students = new List<Student>();

            foreach (var item in items)
            {
                var parts = item.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    students.Add(new Student(parts[0], score));
                }
            }

            var result = students
                .Where(s => s.Score >= minScore)
                .OrderByDescending(s => s.Score)
                .ThenBy(s => s.Name, StringComparer.Ordinal)
                .ToList();

            return JsonSerializer.Serialize(result);
        }
    }
}
