using System.Drawing;

namespace Day9
{
    public class Rope
    {
        public Rope(int numKnots)
        {
            Knots = new Point[numKnots];
        }
        public Point Head => Knots[0];
        public Point Tail => Knots.Last();

        public Point[] Knots { get; }

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'R':
                    Knots[0] = Knots[0].Right();
                    break;
                case 'L':
                    Knots[0] = Knots[0].Left();
                    break;
                case 'U':
                    Knots[0] = Knots[0].Up();
                    break;
                case 'D':
                    Knots[0] = Knots[0].Down();
                    break;
            }
            for (int i = 1; i < Knots.Length; i++)
            {
                Knots[i] = Knots[i].Follow(Knots[i - 1]);
            }
        }
    }

    public static class PointExtensions
    {
        public static Point Up(this Point p)
        {
            return new Point(p.X, p.Y + 1);
        }

        public static Point Down(this Point p)
        {
            return new Point(p.X, p.Y - 1);
        }

        public static Point Left(this Point p)
        {
            return new Point(p.X - 1, p.Y);
        }

        public static Point Right(this Point p)
        {
            return new Point(p.X + 1, p.Y);
        }

        public static Point Follow(this Point p, Point predecessor)
        {
            var (distanceX, distanceY) = (predecessor.X - p.X, predecessor.Y - p.Y);
            if (Math.Abs(distanceX) > 1 || Math.Abs(distanceY) > 1)
            {
                return new Point(p.X + Math.Sign(distanceX), p.Y + Math.Sign(distanceY));
            }
            return p;
        }
    }
}
