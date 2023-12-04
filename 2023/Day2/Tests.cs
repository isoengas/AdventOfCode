using NUnit.Framework;


namespace Day2
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ParseGame()
        {
            var gameRecord = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
            var game = Game.FromRecord(gameRecord);
            var expectedConfigurations = new List<Configuration>
            {
                new(4, 0, 3),
                new(1, 2, 6),
                new(0, 2, 0)
            };
            Assert.That(game.GameId, Is.EqualTo(1));
            Assert.That(game.Configurations, Has.Exactly(3).Items);
            Assert.That(game.Configurations, Is.EqualTo(expectedConfigurations));
        }
    }
}
