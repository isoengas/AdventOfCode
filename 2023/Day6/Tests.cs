using NUnit.Framework;
using static Day6.Program;

namespace Day6
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(7, 9, 0, ExpectedResult = false)]
        [TestCase(7, 9, 1, ExpectedResult = false)]
        [TestCase(7, 9, 2, ExpectedResult = true)]
        [TestCase(7, 9, 3, ExpectedResult = true)]
        [TestCase(7, 9, 4, ExpectedResult = true)]
        [TestCase(7, 9, 5, ExpectedResult = true)]
        [TestCase(7, 9, 6, ExpectedResult = false)]
        [TestCase(7, 9, 7, ExpectedResult = false)]
        public bool Test_isWinner(int raceTime, int maxDistance, int pushTime)
        {
            return new Race(raceTime, maxDistance).IsWinner(pushTime);
        }

        [Test]
        [TestCase(7, 9, ExpectedResult = 4)]
        [TestCase(15, 40, ExpectedResult = 8)]
        [TestCase(30, 200, ExpectedResult = 9)]
        [TestCase(71530, 940200, ExpectedResult = 71503)]
        public int Test_WinningMoves(int raceTime, int maxDistance)
        {
            return new Race(raceTime, maxDistance).WinningMoves();
        }
    }
}
