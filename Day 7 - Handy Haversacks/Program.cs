using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7HandyHaversacks
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var rules = ParseRules(input);

            Console.WriteLine($"Part 1: {PartOne(rules)}");
            Console.WriteLine($"Part 2: {PartTwo(rules)}");
        }

        private static int PartOne(Dictionary<string, List<(int, string)>> rules)
        {
            var validBags = new HashSet<string>();

            var validBagsToFind = new Stack<string>();
            validBagsToFind.Push("shiny gold");

            while (validBagsToFind.Count > 0)
            {
                var validBag = validBagsToFind.Pop();

                foreach (var rule in rules)
                    if (rule.Value.Any(x => x.Item2 == validBag))
                    {
                        validBags.Add(rule.Key);
                        validBagsToFind.Push(rule.Key);
                    }
            }

            return validBags.Count;
        }

        private static int PartTwo(Dictionary<string, List<(int, string)>> rules) =>
            CountBagsInBag(rules, "shiny gold") - 1;

        private static int CountBagsInBag(Dictionary<string, List<(int, string)>> rules, string bag) =>
            rules[bag].Sum(x => x.Item1 * CountBagsInBag(rules, x.Item2)) + 1;

        private static Dictionary<string, List<(int, string)>> ParseRules(string[] input)
        {
            var rules = new Dictionary<string, List<(int, string)>>();

            foreach (var inputLine in input)
            {
                var bagAndSubBagsSplit = inputLine.Split(" bags contain ");
                var bag = bagAndSubBagsSplit[0];

                if (bagAndSubBagsSplit[1] == "no other bags.")
                    rules.Add(bag, new List<(int, string)>());
                else
                {
                    var subBagsSplit = bagAndSubBagsSplit[1].Split(", ")
                        .Select(x => Regex.Replace(x, "bag(s)*", "").Trim(new[] { ' ', '.' }));

                    rules.Add(bag, subBagsSplit.Select(x =>
                    {
                        var split = x.Split(new[] { ' ' }, 2);
                        return (int.Parse(split[0]), split[1]);
                    }).ToList());
                }
            }

            return rules;
        }
    }
}
