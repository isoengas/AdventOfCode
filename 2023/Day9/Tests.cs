using NUnit.Framework;


namespace Day9
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("0 3 6 9 12 15", ExpectedResult = 18)]
        [TestCase("1 3 6 10 15 21", ExpectedResult = 28)]
        [TestCase("10 13 16 21 30 45", ExpectedResult = 68)]
        public int TestExtrapolation(string line)
        {
            var sequence = Program.Parse(line);
            return Program.Extrapolate(sequence);
        }

        [Test]
        [TestCase("0 3 6 9 12 15", ExpectedResult = -3)]
        [TestCase("1 3 6 10 15 21", ExpectedResult = 0)]
        [TestCase("10 13 16 21 30 45", ExpectedResult = 5)]
        public int TestBackExtrapolation(string line)
        {
            var sequence = Program.Parse(line);
            return Program.Extrapolate(sequence.Reverse().ToArray());
        }
    }
}
