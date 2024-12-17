

using System.Text;

namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 6 - Guard Gallivant");
            var map = Map.Parse(File.ReadAllLines("input.txt"));
            //string[] lines = [
            //"....#.....",
            //".........#",
            //"..........",
            //"..#.......",
            //".......#..",
            //"..........",
            //".#..^.....",
            //"........#.",
            //"#.........",
            //"......#...",
            //    ];
            //map = Map.Parse(lines);
            map.Walk();
            int visitedPositions = map.NumVisitedPositions();
            Console.WriteLine("Part 1: " + visitedPositions);
            
            int numObstacles = 0;
            foreach (var (x, y, pos) in map.Iterate().Where(k => k.position.IsVisited))
            {
                var map2 = Map.Parse(File.ReadAllLines("input.txt"));
                //map2 = Map.Parse(lines);
                map2.Add(y, x, new Position(true));
                if (map2.Walk())
                    numObstacles++;
            }
            Console.WriteLine("Part 2: " + numObstacles);
        }
    }

    internal class Map
    {
        private readonly Position[,] _map;
        private int _currentX = 0;
        private int _currentY = 0;
        private Direction _direction;
        private bool _isGuardInLoop = false;
        public Map(int width, int height)
        {
            _map = new Position[width, height];
        }

        internal bool Walk()
        {
            while (IsGuardInside() && !IsGuardInLoop())
            {
                Next();
            }
            return IsGuardInLoop();
        }

        internal IEnumerable<(int x, int y, Position position)> Iterate()
        {
            for (int y = 0; y < _map.GetLength(0); y++)
                for (int x = 0; x < _map.GetLength(1); x++)
                    yield return (x, y, _map[y, x]);
        }

        private bool IsGuardInLoop() => _isGuardInLoop;

        internal static Map Parse(string[] lines)
        {
            var map = new Map(lines[0].Length, lines.Length);
            foreach (var (i, line) in lines.Index())
            {
                foreach (var (j, c) in line.Index())
                {
                    switch (c)
                    {
                        case '.':
                            map.Add(i, j, new Position(false));
                            break;
                        case '#':
                            map.Add(i, j, new Position(true));
                            break;
                        case '^':
                            map.Add(i, j, new Position(false));
                            map.SetCurrent(i, j);
                            map.SetDirection(Direction.North);
                            break;
                    }
                }
            }
            return map;
        }

        private void SetCurrent(int i, int j)
        {
            _currentX = j;
            _currentY = i;
        }

        private void SetDirection(Direction direction)
        {
            _direction = direction;
        }

        internal void Add(int i, int j, Position position)
        {
            _map[i, j] = position;
        }

        internal bool IsGuardInside()
        {
            return _currentX >= 0 && _currentX < _map.GetLength(1) && _currentY >= 0 && _currentY < _map.GetLength(0);
        }

        internal void Next()
        {
            var (deltaX, deltaY) = _direction switch
            {
                Direction.North => (0, -1),
                Direction.East => (1, 0),
                Direction.South => (0, 1),
                Direction.West => (-1, 0),
                _ => (0, 0)
            };
            int nextY = _currentY + deltaY;
            int nextX = _currentX + deltaX;
            if (nextY >= 0 && _map.GetLength(0) > nextY && nextX >= 0 && _map.GetLength(1) > nextX && _map[nextY, nextX].IsBlocker)
            {
                Turn();
            }
            else
            {
                _isGuardInLoop = _map[_currentY, _currentX].Visit(_direction);
                _currentX += deltaX;
                _currentY += deltaY;
            }
        }

        private void Turn()
        {
            _direction = _direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => _direction
            };
        }

        internal int NumVisitedPositions()
        {
            return _map.Cast<Position>().Count(p => p.IsVisited);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1); j++)
                {
                    sb.Append(_map[i, j].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    internal enum Direction
    {
        North,
        East,
        South,
        West
    }

    internal class Position(bool Blocker)
    {
        private bool _isNorthVisited;
        private bool _isEastVisited;
        private bool _isSouthVisited;
        private bool _isWestVisited;
        internal bool Visit(Direction direction)
        {
            IsVisited = true;
            switch (direction)
            {
                case Direction.North when _isNorthVisited:
                    return true;
                case Direction.North:
                    _isNorthVisited = true;
                    return false;
                case Direction.East when _isEastVisited:
                    return true;
                case Direction.East:
                    _isEastVisited = true;
                    return false;
                case Direction.South when _isSouthVisited:
                    return true;
                case Direction.South:
                    _isSouthVisited = true;
                    return false;
                case Direction.West when _isWestVisited:
                    return true;
                default:
                    _isWestVisited = true;
                    return false;
            }
        }
        internal bool IsVisited { get; private set; }
        internal bool IsBlocker => Blocker;
        public override string ToString()
        {
            return Blocker ? "#" : (IsVisited ? "X" : ".");
        }
    }
}
