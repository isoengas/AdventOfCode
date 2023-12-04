namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 1 - Trebuchet?!");
            var lines = File.ReadAllLines("input.txt");
            Console.WriteLine("Part 1: " + lines.Select(ParseLine).Sum());
            Console.WriteLine("Part 2: " + lines.Select(ParseLineWithWords).Sum());
        }

        private static Dictionary<string, string> replaces = new() {
            ["one"] = "1",
            ["two"] = "2",
            ["three"] = "3",
            ["four"] = "4",
            ["five"] = "5",
            ["six"] = "6",
            ["seven"] = "7",
            ["eight"] = "8",
            ["nine"] = "9"
        };

        public static int ParseLineWithWords(string line)
        {
            var mins = replaces
                .Select(r => (r.Key, line.IndexOf(r.Key)))
                .Where(i => i.Item2 >= 0)
                .OrderBy(q => q.Item2);
            if (mins.Any())
                line = line.Substring(0, mins.First().Item2) + replaces[mins.First().Key] + line.Substring(mins.First().Item2);

            var maxs = replaces
                .Select(r => (r.Key, line.LastIndexOf(r.Key)))
                .Where(i => i.Item2 >= 0)
                .OrderByDescending(q => q.Item2);
            if (maxs.Any())
                line = line.Substring(0, maxs.First().Item2 + maxs.First().Key.Length) + replaces[maxs.First().Key] + line.Substring(maxs.First().Item2 + maxs.First().Key.Length);

            return ParseLine(line);
        }

        public static int ParseLine(string line)
        {
            return (line.First(char.IsDigit) - '0') * 10 + (line.Last(char.IsDigit) - '0');
        }
    }
}
