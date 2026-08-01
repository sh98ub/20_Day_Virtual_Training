using System;
using System.Collections.Generic;
using System.Text;

namespace Q22_MahirlAlphabets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter First Word: ");
            string word1 = Console.ReadLine()!;

            Console.Write("Enter Second Word: ");
            string word2 = Console.ReadLine()!;

            string result = ProcessWords(word1, word2);
            Console.WriteLine($"Output: {result}");
        }

        public static string ProcessWords(string word1, string word2)
        {
            if (string.IsNullOrEmpty(word1)) return "";

            HashSet<char> word2Consonants = new HashSet<char>();
            if (!string.IsNullOrEmpty(word2))
            {
                foreach (char c in word2)
                {
                    if (IsConsonant(c))
                        word2Consonants.Add(char.ToLower(c));
                }
            }

            // Task 1: Remove common consonants
            StringBuilder sb = new StringBuilder();
            foreach (char c in word1)
            {
                if (IsConsonant(c) && word2Consonants.Contains(char.ToLower(c)))
                    continue;
                sb.Append(c);
            }

            // Task 2: Remove consecutive duplicates
            StringBuilder finalResult = new StringBuilder();
            foreach (char c in sb.ToString())
            {
                if (finalResult.Length == 0 || char.ToLower(c) != char.ToLower(finalResult[finalResult.Length - 1]))
                {
                    finalResult.Append(c);
                }
            }

            return finalResult.ToString();
        }

        static bool IsVowel(char c)
        {
            c = char.ToLower(c);
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
        }

        static bool IsConsonant(char c)
        {
            return char.IsLetter(c) && !IsVowel(c);
        }
    }
}
