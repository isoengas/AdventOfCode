using NUnit.Framework.Constraints;
using System.Runtime.ExceptionServices;

namespace Day14
{
    internal class Cave
    {
        private readonly int _minX;
        private readonly int _maxX;
        private readonly int _minY;
        private readonly int _maxY;
        private const int Empty = 0;
        private const int Wall = 1;
        private const int Sand = 2;
        private readonly Point SandStartingPoint = new Point(500, 0);
        private int[,] Map { get; }
        private Point CurrentSand { get; set; }

        public Cave((int, int) horizontalBounds, (int, int) verticalBounds)
        {
            (_minX, _maxX) = horizontalBounds;
            (_minY, _maxY) = verticalBounds;
            CurrentSand = SandStartingPoint;
            Map = new int[horizontalBounds.Item2 - horizontalBounds.Item1 + 1, verticalBounds.Item2 - verticalBounds.Item1 + 1];
        }

        private void SetValue(Point point, int value) 
        {
            Map[point.X - _minX, point.Y - _minY] = value;
        }

        private int GetValue(Point point)
        {
            if (point.X < _minX || point.X > _maxX)
            {
                throw new InvalidOperationException($"Sand fell out of bounds: {point.X}, {point.Y}");
            }
            if (point.Y > _maxY) return Empty;
            return Map[point.X - _minX, point.Y - _minY];
        }

        public void AddWalls(Point[] walls)
        {
            for (int i = 0; i < walls.Length -1; i++)
            {
                var from = walls[i];
                var to = walls[i + 1];
                if (from.X == to.X)
                {
                    // Vertical wall
                    for (int j = 0; j <= Math.Abs(to.Y - from.Y); j ++)
                    {
                        SetValue(new Point(from.X, from.Y + j * Math.Sign(to.Y - from.Y)), Wall);
                    }
                }
                else
                {
                    // Horizontal wall
                    for (int j = 0; j <= Math.Abs(to.X - from.X); j ++)
                    {
                        SetValue(new Point(from.X + j * Math.Sign(to.X - from.X), from.Y), Wall);
                    }
                }
            }
        }

        private bool SimulationFinished = false;

        public void StartSimulation()
        {
            while (!SimulationFinished)
            {
                var nextSpot = GetNextSpot();
                if (nextSpot == SandStartingPoint)
                {
                    NumSands++;
                    SimulationFinished = true;
                    continue;
                }
                if (nextSpot == CurrentSand)
                {
                    // At rest
                    CurrentSand = SandStartingPoint;
                    NumSands++;
                    continue;
                }
                SetValue(CurrentSand, Empty);
                if (nextSpot.Y > _maxY)
                {
                    SimulationFinished = true;
                }
                else
                {
                    CurrentSand = nextSpot;
                    SetValue(CurrentSand, Sand);
                }
            }
        }

        public int MaxY
        {
            get
            {
                for (int y =_maxY; y >= _minY; y--)
                {
                    for (int x = _minX; x <= _maxX; x++)
                    {
                        if (GetValue(new Point(x, y)) == Wall) return y;
                    }
                }
                return _minY;
            }
        }


        private Point GetNextSpot()
        {
            if (GetValue(CurrentSand.Below()) == Empty)
                return CurrentSand.Below();
            if (GetValue(CurrentSand.Below().Left()) == Empty)
                return CurrentSand.Below().Left();
            if (GetValue(CurrentSand.Below().Right()) == Empty)
                return CurrentSand.Below().Right();
            return CurrentSand;
        }

        public void DrawMap()
        {
            for (int y = 0; y <= Map.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= Map.GetUpperBound(0); x++)
                {
                    Console.Write(Map[x, y] switch
                    {
                        Wall => "#",
                        Sand => "o",
                        _ => "."
                    });
                }
                Console.WriteLine();
            }
        }

        public int NumSands { get; private set; }
    }

    internal record Point(int X, int Y)
    {
        internal Point Below()
        {
            return new Point(this.X, this.Y + 1);
        }

        internal Point Left()
        {
            return new Point(this.X - 1, this.Y);
        }

        internal Point Right()
        {
            return new Point(this.X + 1, this.Y);
        }
    }
}