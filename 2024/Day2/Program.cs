
namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 2 - Red-Nosed Reports");
            var lines = File.ReadAllLines("input.txt");
            var safeReports = lines.Select(Parse).Count(rep => IsSafe(rep));
            Console.WriteLine("Part 1: " + safeReports);
            var safeReportsWithDampener = lines.Select(Parse).Count(rep => IsSafe(rep, true));
            Console.WriteLine("Part 2: " + safeReportsWithDampener);
        }

        private static bool IsSafe(IEnumerable<int> report, bool useDampener = false)
        {
            var diffs = report.Zip(report.Skip(1), (a, b) => b - a);
            bool? inc = null;
            foreach (var (index, diff) in diffs.Index())
            {
                if (diff == 0 || diff < -3 || diff > 3)
                {
                    var withoutPrevious = report.Take(index - 1).Concat(report.Skip(index));
                    var withoutCurrent = report.Take(index).Concat(report.Skip(index + 1));
                    var withoutNext = report.Take(index + 1).Concat(report.Skip(index + 2));
                    return useDampener && (IsSafe(withoutPrevious) || IsSafe(withoutCurrent) || IsSafe(withoutNext));
                }
                if (diff < 0)
                {
                    if (inc == true)
                    {
                        var withoutPrevious = report.Take(index - 1).Concat(report.Skip(index));
                        var withoutCurrent = report.Take(index).Concat(report.Skip(index + 1));
                        var withoutNext = report.Take(index + 1).Concat(report.Skip(index + 2));
                        return useDampener && (IsSafe(withoutPrevious) || IsSafe(withoutCurrent) || IsSafe(withoutNext));
                    }
                    inc = false;
                }
                else
                {
                    if (inc == false)
                    {
                        var withoutPrevious = report.Take(index - 1).Concat(report.Skip(index));
                        var withoutCurrent = report.Take(index).Concat(report.Skip(index + 1));
                        var withoutNext = report.Take(index + 1).Concat(report.Skip(index + 2));
                        return useDampener && (IsSafe(withoutPrevious) || IsSafe(withoutCurrent) || IsSafe(withoutNext));
                    }
                    inc = true;
                }
            }
            return true;
        }

        private static int[] Parse(string line)
        {
            return line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
    }
}
