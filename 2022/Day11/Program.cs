using Day11;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Day 11 - Monkey in the Middle");
        var lines = File.ReadAllLines("input.txt");
        var monkeys = lines
                            .Select((line, index) => (line, index))
                            .GroupBy(x => x.index / 7)
                            .Select(x => MonkeyParser.Parse(x.Select(y => y.line).Skip(1).ToArray()))
                            .ToArray();

        foreach (var round in Enumerable.Range(0, 20))
        {
            foreach (var monkey in monkeys)
            {
                var throws = monkey.Turn();
                foreach (var @throw in throws)
                {
                    monkeys[@throw.monkey].ThrowItem(@throw.item);
                }
            }
        }
        var mostActiveMonkeys = monkeys.OrderByDescending(m => m.NumInspectedItems).Take(2).ToArray();
        var monkeyBusiness = mostActiveMonkeys[0].NumInspectedItems * mostActiveMonkeys[1].NumInspectedItems;
        Console.WriteLine($"Level of monkey busines is {monkeyBusiness}");

        var linesExample = File.ReadLines("input.txt");
        monkeys = linesExample
                    .Select((line, index) => (line, index))
                    .GroupBy(x => x.index / 7)
                    .Select(x => MonkeyParser.Parse(x.Select(y => y.line).Skip(1).ToArray(), 1))
                    .ToArray();
        long factor = monkeys.Aggregate(1L, (acc, m) => acc * m.Test);
        foreach (var round in Enumerable.Range(0, 10000))
        {
            foreach (var monkey in monkeys)
            {
                var throws = monkey.Turn();
                foreach (var @throw in throws)
                {
                    monkeys[@throw.monkey].ThrowItem(@throw.item % factor);
                }
            }
        }
        mostActiveMonkeys = monkeys.OrderByDescending(m => m.NumInspectedItems).Take(2).ToArray();
        monkeyBusiness = mostActiveMonkeys[0].NumInspectedItems * mostActiveMonkeys[1].NumInspectedItems;
        Console.WriteLine($"Level of monkey busines is {monkeyBusiness}");
    }
}