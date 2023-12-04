// See https://aka.ms/new-console-template for more information
using Day15;
using NUnit.Framework;

Console.WriteLine("Day 15 - Beacon Exclusion Zone");
var lines = File.ReadAllLines("input.txt");
var links = lines.Select(Link.Parse).ToList();
var exclusionRanges = links.SelectMany(l => l.GetExclusionRanges(2000000)).ToList();
int count = 0;
int from = exclusionRanges.Min(r => r.x1);
int to = exclusionRanges.Max(r => r.x2);
for (int i = from; i <= to; i++)
{
    if (exclusionRanges.Any(r => r.x1 <= i && r.x2 >= i)) count++;
}
Console.WriteLine();
Console.WriteLine($"Part 1, number of points: {count}");

long tuningFreq = 0;
int y = 0;
while (y <= 4000000 && tuningFreq == 0)
{
    exclusionRanges = links.SelectMany(l => l.GetExclusionRanges(y, 4000000)).ToList();
    int x = 0;
    while (x < 4000000)
    {
        var currentRange = exclusionRanges.Where(r => r.x1 <= x && r.x2 >= x).OrderByDescending(r => r.x2 - r.x1).FirstOrDefault();
        if (currentRange == default)
        {
            if (!links.Any(l => l.Beacon == new Point(x, y)))
            {
                tuningFreq = x * 4000000L + y;
                x = 4000000;
            }
            else
            {
                x++;
            }
        }
        else
        {
            x = currentRange.x2 + 1;
        }
    }
    y++;
}
Console.WriteLine($"Part 2, tuning frequency: {tuningFreq}");
