using System;
using System.IO;
using System.Linq;

namespace Day9EncodingError
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();

            Console.WriteLine($"Part 1: {PartOne(input)}");
            Console.WriteLine($"Part 2: {PartTwo(input)}");
        }

        private static long PartOne(long[] input) => FindInvalidNumber(input);

        private static long PartTwo(long[] input)
        {
            var invalidNumber = FindInvalidNumber(input);

            int i = 0;
            int j = 0;
            for (; i < input.Length - 1; i++)
            {
                for (j = i + 1; j < input.Length && input.Skip(i).Take(j - i + 1).Sum() < invalidNumber; j++)
                {
                }

                if (input.Skip(i).Take(j - i + 1).Sum() == invalidNumber)
                    break;
            }

            return input[i] + input[j];
        }

        private static long FindInvalidNumber(long[] input)
        {
            var preamble = input.Take(25).ToList();

            foreach (var item in input.Skip(25))
            {
                bool valid = false;

                for (var i = 0; i < preamble.Count - 1 && !valid; i++)
                for (var j = i + 1; j < preamble.Count && !valid; j++)
                {
                    if (preamble[i] + preamble[j] == item)
                        valid = true;
                }

                if (valid)
                {
                    preamble.Add(item);
                    preamble.RemoveAt(0);
                }
                else
                {
                    return item;
                }
            }

            return -1;
        }
    }
}
