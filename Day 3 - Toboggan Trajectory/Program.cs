using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day3TobogganTrajectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").ToArray();

            Console.WriteLine(PartOne(input));

            Console.WriteLine(PartTwo(input));
        }

        private static int PartOne(string[] input)
        {
            return CalculateTreesInTrajectory(input, (3, 1));
        }

        private static int PartTwo(string[] input)
        {
            return CalculateTreesInTrajectory(input, (1, 1)) *
                   CalculateTreesInTrajectory(input, (3, 1)) *
                   CalculateTreesInTrajectory(input, (5, 1)) *
                   CalculateTreesInTrajectory(input, (7, 1)) *
                   CalculateTreesInTrajectory(input, (1, 2));
        }

        private static int CalculateTreesInTrajectory(string[] input, (int, int) trajectory)
        {
            var currentPosition = (0, 0);
            var trees = 0;
            while (currentPosition.Item2 < input.Length - 1)
            {
                currentPosition.Item1 += trajectory.Item1;
                currentPosition.Item2 += trajectory.Item2;

                if (input[currentPosition.Item2][currentPosition.Item1 % input[currentPosition.Item2].Length] == '#')
                    trees++;
            }

            return trees;
        }
    }
}
