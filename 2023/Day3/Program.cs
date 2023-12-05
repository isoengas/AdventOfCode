using System.Drawing;

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 3: Gear Ratios");
            var input = File.ReadAllLines("input.txt");
            List<int> parts = ExtractParts(input);
            Console.WriteLine("Part 1: " + parts.Sum());
            List<int> gears = ExtractGears(input);
            Console.WriteLine("Part 2: " + gears.Sum());
        }

        internal static List<int> ExtractGears(string[] input)
        {
            HashSet<Symbol> gears = new HashSet<Symbol>(ExtractSymbols(input).Where(s => s.IsGear));
            HashSet<(int, Symbol)> partGearPairs = [];
            int y = 0;
            foreach (string line in input)
            {
                int x = 0;
                while (x < line.Length)
                {
                    int? currentNumber = null;
                    bool isPart = false;
                    HashSet<Symbol> adjacentGears = [];
                    while (x < line.Length && char.IsDigit(line[x]))
                    {
                        currentNumber = currentNumber.GetValueOrDefault() * 10 + line[x] - '0';
                        foreach (var gear in GetAdjacentSymbols(x, y, gears))
                            adjacentGears.Add(gear);
                        x++;
                    }
                    if (currentNumber.HasValue)
                    {
                        foreach (var adjacentGear in adjacentGears)
                            partGearPairs.Add((currentNumber.Value, adjacentGear));
                        adjacentGears.Clear();
                        currentNumber = null;
                        isPart = false;
                    }
                    else
                    {
                        x++;
                    }
                }
                y++;
            }

            return partGearPairs
                    .GroupBy(p => p.Item2)
                    .Where(g => g.Count() == 2)
                    .Select(g => g.First().Item1 * g.Last().Item1)
                    .ToList();
        }

        internal static List<int> ExtractParts(string[] input)
        {
            HashSet<Symbol> gears = new HashSet<Symbol>(ExtractSymbols(input));
            List<int> parts = [];
            int y = 0;
            foreach (string line in input)
            {
                int x = 0;
                while (x < line.Length)
                {
                    int? currentNumber = null;
                    bool isPart = false;
                    List<Symbol> adjacentGears = [];
                    while (x < line.Length && char.IsDigit(line[x]))
                    {
                        currentNumber = currentNumber.GetValueOrDefault() * 10 + line[x] - '0';
                        adjacentGears.AddRange(GetAdjacentSymbols(x, y, gears));
                        isPart = isPart || GetAdjacentSymbols(x, y, gears).Any();
                        x++;
                    }
                    if (currentNumber.HasValue && isPart)
                    {
                        parts.Add(currentNumber.Value);
                        currentNumber = null;
                        isPart = false;
                    }
                    else
                    {
                        x++;
                    }
                }
                y++;
            }

            return parts;
        }

        private static IEnumerable<Symbol> GetAdjacentSymbols(int x, int y, HashSet<Symbol> symbols)
        {
            List<Point> adjacentCells = [
                 new(x - 1, y - 1),
                new(x, y - 1),
                new(x + 1, y - 1),
                new(x - 1, y),
                new(x + 1, y),
                new(x - 1, y + 1),
                new(x, y + 1),
                new(x + 1, y + 1)
             ];
            return symbols.Where(s => adjacentCells.Contains(s.location));
        }

        internal static IEnumerable<Symbol> ExtractSymbols(string[] input)
        {
            int y = 0;
            foreach (var line in input)
            {
                foreach (var symbol in ExtractSymbols(line, y))
                {
                    yield return symbol;
                }
                y++;
            }
        }

        private static IEnumerable<Symbol> ExtractSymbols(string line, int y)
        {
            return line
                    .Select((chr, x) => new { chr, x })
                    .Where(p => IsSymbol(p.chr))
                    .Select(s => new Symbol(new Point(s.x, y), s.chr));
        }

        private static bool IsSymbol(char chr)
        {
            return !char.IsDigit(chr) && chr != '.';
        }
    }

    internal record Symbol(Point location, char c)
    {
        public bool IsGear => c == '*';
    }
}
