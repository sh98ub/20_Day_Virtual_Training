using System;

namespace Q24_MergeSortedArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = { 1, 3, 5, 7 };
            int[] arr2 = { 2, 4, 6, 8, 10 };

            int[] merged = MergeSortedArrays(arr1, arr2);

            Console.WriteLine($"Array 1: [{string.Join(", ", arr1)}]");
            Console.WriteLine($"Array 2: [{string.Join(", ", arr2)}]");
            Console.WriteLine($"Merged:  [{string.Join(", ", merged)}]");

            string[] strArr1 = { "apple", "cherry" };
            string[] strArr2 = { "banana", "date", "fig" };

            string[] mergedStr = MergeSortedArrays(strArr1, strArr2);
            Console.WriteLine($"\nMerged Strings: [{string.Join(", ", mergedStr)}]");
        }

        public static T[] MergeSortedArrays<T>(T[] a, T[] b) where T : IComparable<T>
        {
            if (a == null) a = Array.Empty<T>();
            if (b == null) b = Array.Empty<T>();

            T[] merged = new T[a.Length + b.Length];
            int i = 0, j = 0, k = 0;

            while (i < a.Length && j < b.Length)
            {
                if (a[i].CompareTo(b[j]) <= 0)
                {
                    merged[k++] = a[i++];
                }
                else
                {
                    merged[k++] = b[j++];
                }
            }

            while (i < a.Length)
            {
                merged[k++] = a[i++];
            }

            while (j < b.Length)
            {
                merged[k++] = b[j++];
            }

            return merged;
        }
    }
}
