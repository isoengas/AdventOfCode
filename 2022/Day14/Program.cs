// See https://aka.ms/new-console-template for more information
using Day14;

var lines = File.ReadAllLines("input.txt");
var cave = new Cave((450, 600), (0, 170));
foreach (var line in lines)
{
    var points = line.Split(" -> ").Select(p =>
    {
        var parts = p.Split(',');
        return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
    }).ToArray();
    cave.AddWalls(points);
}
//cave.DrawMap();
cave.StartSimulation();
//cave.DrawMap();
Console.WriteLine("Day 14 - Regolith Reservoir");
Console.WriteLine();
Console.WriteLine($"Num sands to rest: {cave.NumSands}");

// Part 2
cave = new Cave((300, 700), (0, 170));
foreach (var line in lines)
{
    var points = line.Split(" -> ").Select(p =>
    {
        var parts = p.Split(',');
        return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
    }).ToArray();
    cave.AddWalls(points);
}
cave.AddWalls(new[] { new Point(300, cave.MaxY + 2), new Point(700, cave.MaxY + 2) });
cave.StartSimulation();
//cave.DrawMap();
Console.WriteLine($"Part 2, num sands to rest: {cave.NumSands}");