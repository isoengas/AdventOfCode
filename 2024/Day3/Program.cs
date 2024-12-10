using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 3 - Mull It Over");
            var line = string.Join(string.Empty, File.ReadAllLines("input.txt"));
            var result = GetTotal(line);
            Console.WriteLine("Part 1: " + result);
            var resultConditionals = GetTotalCond(line, true);
            Console.WriteLine("Part 2: " + resultConditionals);
        }

        static long GetTotal(string line)
        {
            return Regex.Matches(line, @"mul\((?<v1>\d{1,3}),(?<v2>\d{1,3})\)").Select(m => long.Parse(m.Groups["v1"].Value) * long.Parse(m.Groups["v2"].Value)).Sum();
        }

        static long GetTotalCond(string line, bool enabled)
        {
            if (enabled)
            {
                var splitLine = line.Split("don't()");
                return GetTotal(splitLine[0]) + (splitLine.Length > 1 ? GetTotalCond(line.Substring(line.IndexOf("don't()") + 1), false) : 0);
            }
            else
            {
                var splitLine = line.Split("do()");
                    
                return splitLine.Length > 1 ? GetTotalCond(line.Substring(line.IndexOf("do()") + 1), true) : 0;
            }
        }
    }
}
