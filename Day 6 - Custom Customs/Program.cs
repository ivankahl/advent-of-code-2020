using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Day6CustomCustoms
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt")
                .Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x =>
                    x.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)).ToArray();

            Console.WriteLine($"Part 1: {PartOne(input)}");
            Console.WriteLine($"Part 2: {PartTwo(input)}");
        }

        private static int PartOne(string[][] input) => input.Sum(x => string.Join("", x).Distinct().Count());

        private static int PartTwo(string[][] input) => input.Sum(x =>
            x.Aggregate(x.First(), (x, result) => string.Join("", result.Intersect(x))).Length);
    }
}
