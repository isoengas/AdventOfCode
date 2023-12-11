using NUnit.Framework;

namespace Day7
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("32T3K", ExpectedResult = 20)]
        [TestCase("KK677", ExpectedResult = 22)]
        [TestCase("T55J5", ExpectedResult = 30)]
        [TestCase("23456", ExpectedResult = 0)]
        [TestCase("23332", ExpectedResult = 32)]
        [TestCase("AA8AA", ExpectedResult = 40)]
        [TestCase("AAAAA", ExpectedResult = 50)]
        public int TestHandWeight(string cards)
        {
            return new Hand(cards).GetHandWeight();
        }

        [Test]
        [TestCase("2345J", ExpectedResult = 20)]
        [TestCase("KKJ77", ExpectedResult = 32)]
        [TestCase("T55J5", ExpectedResult = 40)]
        [TestCase("23J56", ExpectedResult = 20)]
        [TestCase("J3332", ExpectedResult = 40)]
        [TestCase("AAJAA", ExpectedResult = 50)]
        [TestCase("JJJJJ", ExpectedResult = 50)]
        [TestCase("JJJ77", ExpectedResult = 50)]
        [TestCase("JJJ67", ExpectedResult = 40)]
        [TestCase("JJ667", ExpectedResult = 40)]
        [TestCase("JJ666", ExpectedResult = 50)]
        [TestCase("JJ678", ExpectedResult = 30)]
        public int TestHandWeightWithJokers(string cards)
        {
            return new HandWithJoker(cards).GetHandWeight();
        }

        [Test]
        [TestCase("KK677", "KTJJT", ExpectedResult = 1)]
        public int TestHandComparer(string hand1, string hand2)
        {
            var h1 = new Hand(hand1);
            var h2 = new Hand(hand2);
            return h1.CompareTo(h2) switch
            {
                0 => 0,
                > 0 => 1,
                < 0 => -1
            };
        }

        [Test]
        public void TestSort()
        {
            var hands = new List<Hand>
            {
                new("32T3K"),
                new("T55J5"),
                new("KK677"),
                new("KTJJT"),
                new("QQQJA")
            };
            var sorted = hands.OrderBy(h => h).ToList();
            var expectedResult = new List<Hand>
            {
                new("32T3K"),
                new("KTJJT"),
                new("KK677"),
                new("T55J5"),
                new("QQQJA")
            };
            Assert.That(sorted, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestWinnings()
        {
            var games = new List<Game>
            {
                new(new("32T3K"), 765),
                new(new("T55J5"), 684),
                new(new("KK677"), 28),
                new(new("KTJJT"), 220),
                new(new("QQQJA"), 483)
            };
            var winnings = games.OrderBy(h => h.hand).Select((game, index) => (index + 1) * game.bid).Sum();
            Assert.That(winnings, Is.EqualTo(6440));
        }

        [Test]
        public void TestWinningsWithJoker()
        {
            var games = new List<GameWithJoker>
            {
                new(new("32T3K"), 765),
                new(new("T55J5"), 684),
                new(new("KK677"), 28),
                new(new("KTJJT"), 220),
                new(new("QQQJA"), 483)
            };
            var winnings = games.OrderBy(h => h.hand).Select((game, index) => (index + 1) * game.bid).Sum();
            Assert.That(winnings, Is.EqualTo(5905));
        }
    }
}
