using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    public static class Dijkstra
    {
        public static List<(int, int)> FindShortestRoute(char[,] map, (int, int) start, (int, int) finish)
        {
            var dist = new Dictionary<(int, int), int>();
            var prev = new Dictionary<(int, int), (int, int)?>();
            var q = new List<(int, int)>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    dist[(i, j)] = (i, j) == start ? 0 : int.MaxValue;
                    prev[(i, j)] = null;
                    q.Add((i, j));
                }
            }
            

            while (q.Count > 0)
            {
                var u = q.OrderBy(n => dist[n]).First();
                if (u == finish) break;
                q.Remove(u);
                foreach (var v in GetNeighboursUp(map, u).Where(n => q.Contains(n)))
                {
                    int distance = dist[u] + 1;
                    if (distance < dist[v])
                    {
                        dist[v] = distance;
                        prev[v] = u;
                    }
                }
            }
            var s = new Stack<(int, int)>();
            (int, int) current = finish;
            if (prev[current] != null || current == start)
            {
                while (true)
                {
                    s.Push(current);
                    if (prev[current] == null) break;
                    current = prev[current]!.Value;
                }
            }
            return s.ToList();

        }

        private static IEnumerable<(int, int)> GetNeighboursUp(char[,] map, (int, int) node)
        {
            var (x, y) = node;
            if (y > 0 && CanVisitUp(map[x, y - 1], map[x, y])) yield return (x, y - 1);
            if (x + 1 < map.GetLength(0) && CanVisitUp(map[x + 1, y], map[x, y])) yield return (x + 1, y);
            if (y + 1 < map.GetLength(1) && CanVisitUp(map[x, y + 1], map[x, y])) yield return (x, y + 1);
            if (x > 0 && CanVisitUp(map[x - 1, y], map[x, y])) yield return (x - 1, y);
        }

        private static IEnumerable<(int, int)> GetNeighboursDown(char[,] map, (int, int) node)
        {
            var (x, y) = node;
            if (y > 0 && CanVisitDown(map[x, y - 1], map[x, y])) yield return (x, y - 1);
            if (x + 1 < map.GetLength(0) && CanVisitDown(map[x + 1, y], map[x, y])) yield return (x + 1, y);
            if (y + 1 < map.GetLength(1) && CanVisitDown(map[x, y + 1], map[x, y])) yield return (x, y + 1);
            if (x > 0 && CanVisitDown(map[x - 1, y], map[x, y])) yield return (x - 1, y);
        }

        private static bool CanVisitUp(char to, char from)
        {
            return (from == 'S' && to == 'a') || (from == 'z' && to == 'E') || (to != 'E' && to - from < 2);
        }

        private static bool CanVisitDown(char to, char from)
        {
            if (from == 'a') return false;
            return (from == 'E' && to == 'z') || (from != 'E' && from - to < 2);
        }

        internal static List<(int, int)> FindNearestNode(char[,] map, (int, int) start)
        {
            var dist = new Dictionary<(int, int), int>();
            var prev = new Dictionary<(int, int), (int, int)?>();
            var q = new List<(int, int)>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    dist[(i, j)] = (i, j) == start ? 0 : int.MaxValue;
                    prev[(i, j)] = null;
                    q.Add((i, j));
                }
            }


            while (q.Count > 0)
            {
                var u = q.OrderBy(n => dist[n]).First();
                q.Remove(u);
                foreach (var v in GetNeighboursDown(map, u).Where(n => q.Contains(n)))
                {
                    int distance = dist[u] + 1;
                    if (distance < dist[v])
                    {
                        dist[v] = distance;
                        prev[v] = u;
                    }
                }
            }

            var finish = dist.OrderBy(d => d.Value).Where(d => map[d.Key.Item1, d.Key.Item2] == 'a').First().Key;
            var s = new Stack<(int, int)>();
            (int, int) current = finish;
            if (prev[current] != null || current == start)
            {
                while (true)
                {
                    s.Push(current);
                    if (prev[current] == null) break;
                    current = prev[current]!.Value;
                }
            }
            return s.ToList();
        }
    }
}
