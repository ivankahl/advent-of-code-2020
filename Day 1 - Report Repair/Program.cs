using System;
using System.IO;
using System.Linq;

namespace Day1ReportRepair
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();

            /*
             * Part 1
             */
            Console.WriteLine($"Part 1: {PartOne(input)}");

            /*
             * Part 2
             */
            Console.WriteLine($"Part 2: {PartTwo(input)}");
        }

        private static int PartOne(int[] input)
        {
            for (int i = 0; i < input.Length - 1; i++)
                for (int j = i + 1; j < input.Length; j++)
                    if (input[i] + input[j] == 2020)
                        return input[i] * input[j];

            return -1;
        }

        private static int PartTwo(int[] input)
        {
            for (int i = 0; i < input.Length - 2; i++)
                for (int j = i + 1; j < input.Length - 1; j++)
                    for (int k = j + 1; k < input.Length; k++)
                        if (input[i] + input[j] + input[k] == 2020)
                            return input[i] * input[j] * input[k];

            return -1;
        }
    }
}
