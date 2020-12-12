using System;
using System.IO;
using System.Linq;

namespace Day2PasswordPhilosophy
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tuple is as follows: min, max, character, password
            var input = File.ReadAllLines("input.txt").Select(x =>
            {
                var colonSplit = x.Split(": ");
                var policySplit = colonSplit[0].Split(' ');
                var rangeSplit = policySplit[0].Split('-');

                return (int.Parse(rangeSplit[0]), int.Parse(rangeSplit[1]), policySplit[1][0], colonSplit[1]);
            }).ToArray();

            Console.WriteLine($"Part 1: {PartOne(input)}");

            Console.WriteLine($"Part 2: {PartTwo(input)}");
        }

        private static int PartOne((int, int, char, string)[] input)
        {
            return input.Count(x =>
            {
                var charCount = x.Item4.Count(c => c == x.Item3);
                return charCount >= x.Item1 && charCount <= x.Item2;
            });
        }

        private static int PartTwo((int, int, char, string)[] input)
        {
            return input.Count(x => (x.Item4[x.Item1 - 1] == x.Item3 || x.Item4[x.Item2 - 1] == x.Item3) && x.Item4[x.Item1 - 1] != x.Item4[x.Item2 - 1]);
        }
    }
}
