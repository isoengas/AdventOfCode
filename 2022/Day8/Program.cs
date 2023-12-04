using Newtonsoft.Json.Linq;

namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputLines = File.ReadAllLines("input.txt");
            var numCols = inputLines[0].Length;

            var grid = new Grid(numCols);
            foreach (string line in inputLines)
            {
                grid.AddRow(line.Select(c => c - '0'));
            }

            var numVisible = grid.EnumerateItems().Count(item => grid.IsItemVisible(item.x, item.y));
            Console.WriteLine("Day 8 - Treetop Tree House");
            Console.WriteLine($"Number of visible trees: {numVisible}");

            var maxScore = grid.EnumerateItems().Max(item => grid.ScenicScore(item.x, item.y));
            Console.WriteLine($"Highest scenic score is {maxScore}");
        }
    }
}