


namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 4 - Mull It Over");
            var lines = File.ReadAllLines("input.txt");
            int numXmas = Search(lines);
            Console.WriteLine("Part 1: " + numXmas);
            numXmas = SearchX(lines);
            Console.WriteLine("Part 2: " + numXmas);
        }

        private static int SearchX(string[] lines)
        {
            int count = 0;
            var width = lines[0].Length;
            var height = lines.Length;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (lines[y][x] == 'A')
                    {
                        if ((Contains(lines, x - 1, y - 1, 'M') && Contains(lines, x + 1, y + 1, 'S')) ||
                            (Contains(lines, x - 1, y - 1, 'S') && Contains(lines, x + 1, y + 1, 'M')))
                        {
                            if ((Contains(lines, x + 1, y - 1, 'M') && Contains(lines, x - 1, y + 1, 'S')) ||
                                (Contains(lines, x + 1, y - 1, 'S') && Contains(lines, x - 1, y + 1, 'M')))
                            {
                                count++;
                            }
                        }

                    }
                }
            }
            return count;
        }

        private static int Search(string[] lines)
        {
            int count = 0;
            var width = lines[0].Length;
            var height = lines.Length;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (lines[y][x] == 'X')
                    {
                        // EAST
                        if (Contains(lines, x + 1, y, 'M') && Contains(lines, x + 2, y, 'A') && Contains(lines, x + 3, y, 'S'))
                            count++;
                        // WEST
                        if (Contains(lines, x - 1, y, 'M') && Contains(lines, x - 2, y, 'A') && Contains(lines, x - 3, y, 'S'))
                            count++;
                        // NORTH
                        if (Contains(lines, x, y - 1, 'M') && Contains(lines, x, y - 2, 'A') && Contains(lines, x, y - 3, 'S'))
                            count++;
                        // SOUTH
                        if (Contains(lines, x, y + 1, 'M') && Contains(lines, x, y + 2, 'A') && Contains(lines, x, y + 3, 'S'))
                            count++;
                        // NORTH EAST
                        if (Contains(lines, x + 1, y - 1, 'M') && Contains(lines, x + 2, y - 2, 'A') && Contains(lines, x + 3, y - 3, 'S'))
                            count++;
                        // NORTH WEST
                        if (Contains(lines, x - 1, y - 1, 'M') && Contains(lines, x - 2, y - 2, 'A') && Contains(lines, x - 3, y - 3, 'S'))
                            count++;
                        // SOUTH EAST
                        if (Contains(lines, x + 1, y + 1, 'M') && Contains(lines, x + 2, y + 2, 'A') && Contains(lines, x + 3, y + 3, 'S'))
                            count++;
                        // SOUTH WEST
                        if (Contains(lines, x - 1, y + 1, 'M') && Contains(lines, x - 2, y + 2, 'A') && Contains(lines, x - 3, y + 3, 'S'))
                            count++;
                    }
                }
            }
            return count;
        }

        private static bool Contains(string[] lines, int x, int y, char c)
        {
            if (y < 0 || y >= lines.Length) return false;
            if (x < 0 || x >= lines[y].Length) return false;
            return lines[y][x] == c;
        }
    }
}
