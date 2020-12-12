using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5BinaryBoarding
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").ToArray();

            Console.WriteLine($"Part 1: {PartOne(input)}");
            Console.WriteLine($"Part 2: {PartTwo(input)}");
        }

        private static int PartOne(string[] input) => input.Max(CalculateSeatId);

        private static int CalculateSeatId(string booking)
        {
            var rowBound = (0, 127);
            var colBound = (0, 7);

            foreach (var c in booking)
                switch (c)
                {
                    case 'F':
                        rowBound.Item2 = Convert.ToInt32(Math.Floor((rowBound.Item1 + rowBound.Item2) / 2.0));
                        break;
                    case 'B':
                        rowBound.Item1 = Convert.ToInt32(Math.Ceiling((rowBound.Item1 + rowBound.Item2) / 2.0));
                        break;
                    case 'R':
                        colBound.Item1 = Convert.ToInt32(Math.Ceiling((colBound.Item1 + colBound.Item2) / 2.0));
                        break;
                    case 'L':
                        colBound.Item2 = Convert.ToInt32(Math.Floor((colBound.Item1 + colBound.Item2) / 2.0));
                        break;
                }

            return rowBound.Item1 * 8 + colBound.Item1;
        }

        private static int PartTwo(string[] input)
        {
            var seatIds = input.Select(CalculateSeatId).OrderBy(x => x).ToArray();

            for (var i = 0; i < seatIds.Length - 1; i++)
                if (seatIds[i] + 1 != seatIds[i + 1])
                    return seatIds[i] + 1;

            return -1;
        }
    }
}
