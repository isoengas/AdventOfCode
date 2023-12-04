using NUnit.Framework;

namespace Day11
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void TestMonkeyTurn()
        {
            var monkey = new Monkey(new long[] { 79, 98 }, "old * 19", 23, 2, 3);
            var result = monkey.Turn();
            var expectedResult = new[] { new Throw(500, 3), new Throw(620, 3) };
            Assert.That(result, Is.EquivalentTo(expectedResult));
        }

        [Test]
        public void TestMonkeyParser()
        {
            var input = new[]
            {
                "  Starting items: 71, 86",
                "  Operation: new = old * 13",
                "  Test: divisible by 19",
                "    If true: throw to monkey 6",
                "    If false: throw to monkey 7"
            };
            var monkey = MonkeyParser.Parse(input);
            Assert.That(monkey.Items, Is.EquivalentTo(new[] { 71, 86 }));
            Assert.That(monkey.Operation, Is.EqualTo("old * 13"));
            Assert.That(monkey.Test, Is.EqualTo(19));
            Assert.That(monkey.IfTrue, Is.EqualTo(6));
            Assert.That(monkey.IfFalse, Is.EqualTo(7));
        }

        [Test]
        public void TestExampleInput()
        {
            var lines = File.ReadAllLines("exampleInput.txt");
            var monkeys = lines
                                .Select((line, index) => (line, index))
                                .GroupBy(x => x.index / 7)
                                .Select(x => MonkeyParser.Parse(x.Select(y => y.line).Skip(1).ToArray()))
                                .ToArray();
            Assert.That(monkeys, Has.Exactly(4).Items);
            foreach (var monkey in monkeys)
            {
                var throws = monkey.Turn();
                foreach (var @throw in throws)
                {
                    monkeys[@throw.monkey].ThrowItem(@throw.item);
                }
            }
            Assert.That(monkeys[0].Items, Is.EquivalentTo(new[] { 20, 23, 27, 26 }));
            Assert.That(monkeys[1].Items, Is.EquivalentTo(new[] { 2080, 25, 167, 207, 401, 1046 }));
            Assert.That(monkeys[2].Items, Is.Empty);
            Assert.That(monkeys[3].Items, Is.Empty);
        }

        [Test]
        public void Test20Rounds()
        {
            var lines = File.ReadAllLines("exampleInput.txt");
            var monkeys = lines
                                .Select((line, index) => (line, index))
                                .GroupBy(x => x.index / 7)
                                .Select(x => MonkeyParser.Parse(x.Select(y => y.line).Skip(1).ToArray()))
                                .ToArray();
            foreach (var round in Enumerable.Range(0, 20))
            {
                foreach (var monkey in monkeys)
                {
                    var throws = monkey.Turn();
                    foreach (var @throw in throws)
                    {
                        monkeys[@throw.monkey].ThrowItem(@throw.item);
                    }
                }
            }
            Assert.That(monkeys[0].NumInspectedItems, Is.EqualTo(101));
            Assert.That(monkeys[1].NumInspectedItems, Is.EqualTo(95));
            Assert.That(monkeys[2].NumInspectedItems, Is.EqualTo(7));
            Assert.That(monkeys[3].NumInspectedItems, Is.EqualTo(105));
        }

        [Test]
        [TestCase(1, 2, 4, 3, 6)]
        [TestCase(20, 99, 97, 8, 103)]
        [TestCase(1000, 5204, 4792, 199, 5192)]
        [TestCase(2000, 10419, 9577, 392, 10391)]
        public void TestExampleInputPart2(int numRounds, int m0, int m1, int m2, int m3)
        {
            var lines = File.ReadAllLines("exampleInput.txt");
            var monkeys = lines
                                .Select((line, index) => (line, index))
                                .GroupBy(x => x.index / 7)
                                .Select(x => MonkeyParser.Parse(x.Select(y => y.line).Skip(1).ToArray(), 1))
                                .ToArray();
            Assert.That(monkeys, Has.Exactly(4).Items);
            long factor = 19 * 23 * 13 * 17;
            foreach (var round in Enumerable.Range(0, numRounds))
            {
                foreach (var monkey in monkeys)
                {
                    var throws = monkey.Turn();
                    foreach (var @throw in throws)
                    {
                        monkeys[@throw.monkey].ThrowItem(@throw.item % factor);
                    }
                }
            }
            Assert.That(monkeys[0].NumInspectedItems, Is.EqualTo(m0));
            Assert.That(monkeys[1].NumInspectedItems, Is.EqualTo(m1));
            Assert.That(monkeys[2].NumInspectedItems, Is.EqualTo(m2));
            Assert.That(monkeys[3].NumInspectedItems, Is.EqualTo(m3));

        }
    }
}
