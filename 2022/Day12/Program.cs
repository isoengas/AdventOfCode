// See https://aka.ms/new-console-template for more information
using Day12;

var lines = File.ReadAllLines("input.txt");
var map = new char[lines.Length, lines[0].Length];
var start = (0, 0);
var finish = (0, 0);
for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        map[i, j] = lines[i][j];
        if (map[i, j] == 'S') start = (i, j);
        else if (map[i,j] == 'E') finish = (i, j);
    }
}
Console.WriteLine("Day 12 - Hill Climbing Algorithm");
var result = Dijkstra.FindShortestRoute(map, start, finish);
Console.WriteLine($"Took {result.Count - 1} steps!");
var result2 = Dijkstra.FindNearestNode(map, finish);
Console.WriteLine($"Nearest 'a' square is {result2.Count - 1} steps away");