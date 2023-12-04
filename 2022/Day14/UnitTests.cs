using NUnit.Framework;

namespace Day14
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void TestPart1ExampleInput()
        {
            var cave = new Cave((490, 510), (0, 9));
            cave.AddWalls(new[] { new Point(498, 4), new Point(498, 6), new Point(496, 6) });
            cave.AddWalls(new[] { new Point(503, 4), new Point(502, 4), new Point(502, 9), new Point(494, 9) });
            cave.StartSimulation();

            Assert.That(cave.NumSands, Is.EqualTo(24));
        }

        [Test]
        public void TestLowerWall()
        {
            var cave = new Cave((490, 510), (0, 9));
            cave.AddWalls(new[] { new Point(498, 4), new Point(498, 6), new Point(496, 6) });
            cave.AddWalls(new[] { new Point(503, 4), new Point(502, 4), new Point(502, 9), new Point(494, 9) });

            Assert.That(cave.MaxY, Is.EqualTo(9));
        }

        [Test]
        public void TestPart2ExampleInput()
        {
            var cave = new Cave((480, 520), (0, 12));
            cave.AddWalls(new[] { new Point(498, 4), new Point(498, 6), new Point(496, 6) });
            cave.AddWalls(new[] { new Point(503, 4), new Point(502, 4), new Point(502, 9), new Point(494, 9) });
            cave.AddWalls(new[] { new Point(480, cave.MaxY + 2), new Point(520, cave.MaxY + 2) });
            cave.StartSimulation();
            cave.DrawMap();

            Assert.That(cave.NumSands, Is.EqualTo(93));
        }
    }
}
