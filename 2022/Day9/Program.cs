using System.Drawing;

namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rope = new Rope(2);
            var movements = File.ReadAllLines("input.txt");
            var visitedPoints = new List<Point>();
            foreach (var movement in movements)
            {
                char direction = movement[0];
                int times = int.Parse(movement.Split(' ')[1]);
                for (int i = 0; i < times; i++)
                {
                    rope.Move(direction);
                    visitedPoints.Add(rope.Tail);
                }
            }
            var uniquePoints = visitedPoints.Distinct();
            Console.WriteLine("Day 9 - Rope Bridge");
            Console.WriteLine($"Unique visited positions: {uniquePoints.Count()}");

            var rope2 = new Rope(10);
            visitedPoints = new List<Point>();
            foreach (var movement in movements)
            {
                char direction = movement[0];
                int times = int.Parse(movement.Split(' ')[1]);
                for (int i = 0; i < times; i++)
                {
                    rope2.Move(direction);
                    visitedPoints.Add(rope2.Tail);
                }
            }
            uniquePoints = visitedPoints.Distinct();
            Console.WriteLine($"Unique visited positions (10 knots): {uniquePoints.Count()}");
        }
    }
}