// See https://aka.ms/new-console-template for more information
using Day13;
using System.Text.Json.Nodes;

Console.WriteLine("Day 13 - Distress Signal");
var lines = File.ReadAllLines("input.txt");
var groups = lines
                .Select((l, i) => new { Line = l, Index = i })
                .GroupBy(x => x.Index / 3)
                .Select((g, index) => new { Packet1 = g.First().Line, Packet2 = g.Skip(1).First().Line, Index = index + 1 });
int sum = 0;
var packetComparer = new PacketComparer();
foreach (var pair in groups)
{
    var packet1 = JsonNode.Parse(pair.Packet1)!.AsArray();
    var packet2 = JsonNode.Parse(pair.Packet2)!.AsArray();
    if (packetComparer.Compare(packet1, packet2) == -1)
        sum += pair.Index;
}

Console.WriteLine($"The sum of indices in {sum}");

lines = File.ReadAllLines("input.txt");
var divider1 = JsonNode.Parse("[[2]]")!.AsArray();
var divider2 = JsonNode.Parse("[[6]]")!.AsArray();
var orderedPackets = lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => JsonNode.Parse(l)!.AsArray())
                    .Concat(new[]
                    {
                        divider1,
                        divider2
                    })
                    .OrderBy(n => n, packetComparer)
                    .ToList();
var index1 = orderedPackets.IndexOf(divider1) + 1;
var index2 = orderedPackets.IndexOf(divider2) + 1;

Console.WriteLine($"The decoder key is {index1 * index2}");


