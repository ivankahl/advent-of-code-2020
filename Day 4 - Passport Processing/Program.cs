using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Day4PassportProcessing
{
    class Program
    {
        private static string[] RequiredFields = new[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt")
                .Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x =>
                    x.Replace(Environment.NewLine, " ")
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .ToDictionary(x => x.Split(':')[0], x => x.Split(':')[1])).ToArray();

            Console.WriteLine(PartOne(input));

            Console.WriteLine(PartTwo(input));
        }

        private static int PartOne(Dictionary<string, string>[] input) =>
            input.Count(x => x.Keys.Intersect(RequiredFields).Count() == RequiredFields.Length);

        private static int PartTwo(Dictionary<string, string>[] input) =>
            input.Count(x => !(x.Keys.Intersect(RequiredFields).Count() != RequiredFields.Length ||
                               !(int.TryParse(x["byr"], out int birthYear) && birthYear is >= 1920 and <= 2002) ||
                               !(int.TryParse(x["iyr"], out int issueYear) && issueYear is >= 2010 and <= 2020) ||
                               !(int.TryParse(x["eyr"], out int expiryYear) && expiryYear is >= 2020 and <= 2030) ||
                               !(int.TryParse(string.Join("", x["hgt"].Where(char.IsDigit)), out int measurement) &&
                                 (x["hgt"].EndsWith("cm") && measurement is >= 150 and <= 193 ||
                                  x["hgt"].EndsWith("in") && measurement is >= 59 and <= 76)) ||
                               !Regex.IsMatch(x["hcl"], "^#[0-9a-f]{6}$") ||
                               !new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(x["ecl"]) ||
                               !(x["pid"].Length == 9 && int.TryParse(x["pid"], out _))));
    }
}
