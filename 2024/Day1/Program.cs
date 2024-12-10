using System.Linq;
using System.Runtime.CompilerServices;

namespace Day1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Day 1 - Historian Hysteria");
            var lines = File.ReadAllLines("input.txt");
            var (list1, list2) = Parse(lines);
            list1.Sort();
            list2.Sort();
            var result = list1.Zip(list2, (i1, i2) => Math.Abs(i1 - i2)).Sum();
            Console.WriteLine("Part 1: " + result);
            var times = list2.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count());
            var simmilarity = list1.Aggregate(0, (sum, n) => sum + (n * (times.TryGetValue(n, out var m) ? m : 0)));
            Console.WriteLine("Part 1: " + simmilarity);
        }

        private static (List<int> list1, List<int> list2) Parse(string[] lines)
        {
            List<int> l1 = new();
            List<int> l2 = new();

            foreach (var line in lines)
            {
                l1.Add(int.Parse(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]));
                l2.Add(int.Parse(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]));
            }
            return (l1, l2);
        }
    }
}