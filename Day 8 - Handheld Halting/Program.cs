using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

namespace Day8HandheldHalting
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt").Select(x =>
            {
                var s = x.Split(' ');
                return (s[0], int.Parse(s[1]));
            }).ToArray();

            Console.WriteLine($"Part 1: {PartOne(input)}");
            Console.WriteLine($"Part 2: {PartTwo(input)}");
        }

        private static int PartOne((string, int)[] input)
        {
            return RunProgram(input).Item2;
        }

        private static int PartTwo((string, int)[] input)
        {
            // Let's first try and get all the different mutations
            var possibleInstructionSets = new List<(string, int)[]>();

            // First find all the potential mutations
            for (var i = 0; i < input.Length; i++)
            {
                // We need to add the original instruction set
                possibleInstructionSets.Add(input.Select(x => (x.Item1, x.Item2)).ToArray());

                // If the instruction is "nop" or "jmp", mutate the instruction set and also add that
                var altInstructions = input.Select(x => (x.Item1, x.Item2)).ToArray();
                switch (input[i].Item1)
                {
                    case "nop":
                        altInstructions[i].Item1 = "jmp";
                        possibleInstructionSets.Add(altInstructions);
                        break;
                    case "jmp":
                        altInstructions[i].Item1 = "nop";
                        possibleInstructionSets.Add(altInstructions);
                        break;
                }
            }

            // Start executing each program until we manage to complete the run
            foreach (var instructionSet in possibleInstructionSets)
            {
                var result = RunProgram(instructionSet);

                if (result.Item1 == "COMPLETE")
                    return result.Item2;
            }

            return -1;
        }

        public static (string, int, List<int>) RunProgram((string, int)[] input)
        {
            var acc = 0;
            var visitedInstructions = new List<int>();
            var instruction = 0;
            while (!visitedInstructions.Contains(instruction) && instruction < input.Length)
            {
                visitedInstructions.Add(instruction);

                ExecuteInstruction(input[instruction], ref instruction, ref acc);
            }

            return instruction >= input.Length ? ("COMPLETE", acc, visitedInstructions) : ("INCOMPLETE", acc, visitedInstructions);
        }

        private static void ExecuteInstruction((string, int) instruction, ref int nextInstruction, ref int acc)
        {
            switch (instruction.Item1)
            {
                case "acc":
                    acc += instruction.Item2;
                    nextInstruction++;
                    break;
                case "jmp":
                    nextInstruction += instruction.Item2;
                    break;
                case "nop":
                    nextInstruction++;
                    break;
            }
        }
    }
}
