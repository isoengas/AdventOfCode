using NUnit.Framework;

namespace Day2
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        [TestCase('A','Y', 8)]
        [TestCase('B', 'X', 1)]
        [TestCase('C', 'Z', 6)]
        public void Score_should_be_ok(char opponent, char me, int score)
        {
            var round = new Game(opponent, me);
            Assert.That(round.TotalScore, Is.EqualTo(score));
        }

        [Test]
        [TestCase('A', 'Y', 4)]
        [TestCase('B', 'X', 1)]
        [TestCase('C', 'Z', 7)]
        public void Score_2_should_be_ok(char opponent, char result, int score)
        {
            var round = Game.FromResult(opponent, result);
            Assert.That(round.TotalScore, Is.EqualTo(score));
        }

    }
}
