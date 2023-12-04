using NUnit.Framework;
using System.Drawing;

namespace Day9
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void TestExampleInput()
        {
            var rope = new Rope(2);
            List<Point> visitedPoints = new List<Point>(new[] { rope.Tail });
            var movements = "RRRRUUUULLLDRRRRDLLLLLRR";
            foreach (var movement in movements)
            {
                rope.Move(movement);
                visitedPoints.Add(rope.Tail);
            }
            var uniquePoints = visitedPoints.Distinct();
            Assert.That(uniquePoints, Has.Exactly(13).Items);
        }

        [Test]
        public void Test10KnotsExampleInput()
        {
            var rope = new Rope(10);
            List<Point> visitedPoints = new List<Point>(new[] { rope.Tail });
            var movements = "RRRRUUUULLLDRRRRDLLLLLRR";
            foreach (var movement in movements)
            {
                rope.Move(movement);
                visitedPoints.Add(rope.Tail);
            }
            var uniquePoints = visitedPoints.Distinct();
            Assert.That(uniquePoints, Has.Exactly(1).Items);
        }

        [Test]
        public void Test10KnotsExampleInputShort()
        {
            var rope = new Rope(10);
            var movements = "RRRRUU";
            foreach (var movement in movements)
            {
                rope.Move(movement);
            }
            Assert.That(rope.Knots[0], Is.EqualTo(new Point(4, 2)));
            Assert.That(rope.Knots[1], Is.EqualTo(new Point(4, 1)));
            Assert.That(rope.Knots[2], Is.EqualTo(new Point(3, 1)));
            Assert.That(rope.Knots[3], Is.EqualTo(new Point(2, 1)));
        }

        [Test]
        public void Test10KnotsBigInput()
        {
            var rope = new Rope(10);
            List<Point> visitedPoints = new List<Point>(new[] { rope.Tail });
            var movements = new string('R', 5) +
                            new string('U', 8) +
                            new string('L', 8) +
                            new string('D', 3) +
                            new string('R', 17) +
                            new string('D', 10) +
                            new string('L', 25) +
                            new string('U', 20);
            foreach (var movement in movements)
            {
                rope.Move(movement);
                visitedPoints.Add(rope.Tail);
            }
            var uniquePoints = visitedPoints.Distinct();
            Assert.That(uniquePoints, Has.Exactly(36).Items);
        }
    }
}
