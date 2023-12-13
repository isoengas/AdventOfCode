

namespace Day10
{
    internal class Program
    {
        static Dictionary<char, char[]> Connections = new()
        {
            ['N'] = ['|', '7', 'F'],
            ['S'] = ['|', 'J', 'L'],
            ['W'] = ['-', 'F', 'L'],
            ['E'] = ['-', 'J', '7']
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Day 10: Pipe Maze");
            var lines = File.ReadAllLines("input.txt");
            var maze = lines.Select(l => l.ToArray()).ToArray();
            int startX = 111;
            int startY = 62;
            
            int steps = 1;
            int x = 111;
            int y = 61;
            char prevStep = 'N';
            while (maze[y][x] != 'S')
            {
                prevStep = GetNextStep(maze[y][x], prevStep);
                switch (prevStep)
                {
                    case 'N':
                        y--;
                        break;
                    case 'S':
                        y++;
                        break;
                    case 'E':
                        x++;
                        break;
                    case 'W':
                        x--;
                        break;
                }
                steps++;
            }
            Console.WriteLine("Part 1: " + steps / 2);
        }

        private static char GetNextStep(char pipe, char prevStep)
        {
            switch (pipe)
            {
                case '|' when prevStep == 'N':
                    return 'N';
                case '|' when prevStep == 'S':
                    return 'S';
                case '7' when prevStep == 'N':
                    return 'W';
                case '7' when prevStep == 'E':
                    return 'S';
                case 'L' when prevStep == 'S':
                    return 'E';
                case 'L' when prevStep == 'W':
                    return 'N';
                case '-' when prevStep == 'W':
                    return 'W';
                case '-' when prevStep == 'E':
                    return 'E';
                case 'F' when prevStep == 'W':
                    return 'S';
                case 'F' when prevStep == 'N':
                    return 'E';
                case 'J' when prevStep == 'S':
                    return 'W';
                case 'J' when prevStep == 'E':
                    return 'N';
                default:
                    throw new InvalidOperationException("Invalid path found");
            }
        }
    }
}
