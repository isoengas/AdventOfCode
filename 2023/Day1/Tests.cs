using NUnit.Framework;

namespace Day1
{
    [TestFixture]
    internal class Tests
    {
        [Test]
        [TestCase("1abc2", ExpectedResult = 12)]
        [TestCase("pqr3stu8vwx", ExpectedResult = 38)]
        [TestCase("a1b2c3d4e5f", ExpectedResult = 15)]
        [TestCase("treb7uchet", ExpectedResult = 77)]
        [TestCase("two1nine", ExpectedResult = 29)]
        [TestCase("eightwothree", ExpectedResult = 83)]
        [TestCase("abcone2threexyz", ExpectedResult = 13)]
        [TestCase("xtwone3four", ExpectedResult = 24)]
        [TestCase("4nineeightseven2", ExpectedResult = 42)]
        [TestCase("zoneight234", ExpectedResult = 14)]
        [TestCase("2svpbhrlhfjhbkf3fourvvspkfmbvztmtpcxndfnine9", ExpectedResult = 29)]
        [TestCase("one", ExpectedResult = 11)]
        [TestCase("oneight", ExpectedResult = 18)]
        public int TestParseLine(string line)
        {
            return Program.ParseLineWithWords(line);
        }
    }
}
