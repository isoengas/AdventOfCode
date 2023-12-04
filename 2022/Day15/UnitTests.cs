using NUnit.Framework;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day15
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        [TestCase(0,0,0,0, ExpectedResult = 0)]
        [TestCase(0,0,1,1, ExpectedResult = 2)]
        [TestCase(8,7,2,10, ExpectedResult = 9)]
        public int TestDistance(int x1, int y1, int x2, int y2)
        {
            return new Point(x1, y1) - new Point(x2, y2);
        }


        [Test]
        [TestCase(0, 9, ExpectedResult = false)]
        [TestCase(-2, 9, ExpectedResult = true)]
        [TestCase(-3, 10, ExpectedResult = true)]
        [TestCase(-2, 10, ExpectedResult = false)]
        [TestCase(14, 11, ExpectedResult = true)]
        public bool TestCanBeaconExist(int x, int y)
        {
            var links = new[]
            {
                new Link((2, 18), (-2, 15)),
                new Link((9, 16), (10, 16)),
                new Link((13, 2), (15, 3)),
                new Link((12, 14), (10, 16)),
                new Link((10, 20), (10, 16)),
                new Link((14, 17), (10, 16)),
                new Link((8, 7), (2, 10)),
                new Link((2, 0), (2, 10)),
                new Link((0, 11), (2, 10)),
                new Link((20, 14), (25, 17)),
                new Link((17, 20), (21, 22)),
                new Link((16, 7), (15, 3)),
                new Link((14, 3), (15, 3)),
                new Link((20, 1), (15, 3))
            };
            var beaconToTest = new Point(x, y);
            return beaconToTest.CanExist(links);
        }

        [Test]
        [TestCase(8, 7, 2, 10, 10, 3, 14)]
        [TestCase(8, 7, 2, 10, 7, -1, 17)]
        public void TestExclusionRange(int sx, int sy, int bx, int by, int line, int expectedFrom, int expectedTo)
        {
            var link = new Link((sx, sy), (bx, by));
            var exclusionRanges = link.GetExclusionRanges(line);
            Assert.That(exclusionRanges, Has.One.Items);
            Assert.That(exclusionRanges.First(), Is.EqualTo((expectedFrom, expectedTo)));
        }

        [Test]
        public void TestExamplePart1()
        {
            var links = new[]
            {
                new Link((2, 18), (-2, 15)),
                new Link((9, 16), (10, 16)),
                new Link((13, 2), (15, 3)),
                new Link((12, 14), (10, 16)),
                new Link((10, 20), (10, 16)),
                new Link((14, 17), (10, 16)),
                new Link((8, 7), (2, 10)),
                new Link((2, 0), (2, 10)),
                new Link((0, 11), (2, 10)),
                new Link((20, 14), (25, 17)),
                new Link((17, 20), (21, 22)),
                new Link((16, 7), (15, 3)),
                new Link((14, 3), (15, 3)),
                new Link((20, 1), (15, 3))
            };
            var exclusionRanges = links.SelectMany(l => l.GetExclusionRanges(10)).ToList();
            int count = 0;
            int from = exclusionRanges.Min(r => r.x1);
            int to = exclusionRanges.Max(r => r.x2);
            for (int i = from; i <= to; i++)
            {
                if (exclusionRanges.Any(r => r.x1 <= i && r.x2 >= i)) count++;
            }
            Assert.That(count, Is.EqualTo(26));
        }

        [Test]
        public void TestExamplePart2()
        {
            var links = new[]
            {
                new Link((2, 18), (-2, 15)),
                new Link((9, 16), (10, 16)),
                new Link((13, 2), (15, 3)),
                new Link((12, 14), (10, 16)),
                new Link((10, 20), (10, 16)),
                new Link((14, 17), (10, 16)),
                new Link((8, 7), (2, 10)),
                new Link((2, 0), (2, 10)),
                new Link((0, 11), (2, 10)),
                new Link((20, 14), (25, 17)),
                new Link((17, 20), (21, 22)),
                new Link((16, 7), (15, 3)),
                new Link((14, 3), (15, 3)),
                new Link((20, 1), (15, 3))
            };
            int tuningFreq = 0;
            int y = 0;
            while (y <= 20 && tuningFreq == 0)
            {
                var exclusionRanges = links.SelectMany(l => l.GetExclusionRanges(y, 20)).ToList();
                int x = 0;
                while (x < 20)
                {
                    var currentRange = exclusionRanges.Where(r => r.x1 <= x && r.x2 >= x).OrderByDescending(r => r.x2 - r.x1).FirstOrDefault();
                    if (currentRange == default)
                    {
                        if (!links.Any(l => l.Beacon == new Point(x, y)))
                        {
                            tuningFreq = x * 4000000 + y;
                            x = 20;
                        }
                        else
                        {
                            x++;
                        }
                    }
                    else
                    {
                        x = currentRange.x2 + 1;
                    }
                }
                y++;
            }

            Assert.That(tuningFreq, Is.EqualTo(56000011));
        }

        [Test]
        public void TestParseInput()
        {
            var input = "Sensor at x=505, y=67828: closest beacon is at x=-645204, y=289136";
            Link link = Link.Parse(input);
            Assert.That(link.Sensor, Is.EqualTo(new Point(505, 67828)));
            Assert.That(link.Beacon, Is.EqualTo(new Point(-645204, 289136)));
        }
    }
}
