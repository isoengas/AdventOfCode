using NUnit.Framework;

namespace Day4
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        [TestCase("2-4", "6-8", ExpectedResult = false)]
        [TestCase("2-3", "4-5", ExpectedResult = false)]
        [TestCase("5-7", "7-9", ExpectedResult = false)]
        [TestCase("2-8", "3-7", ExpectedResult = true)]
        [TestCase("6-6", "4-6", ExpectedResult = true)]
        [TestCase("2-6", "4-8", ExpectedResult = false)]
        public bool TestRangeFullyContains(string range1Text, string range2Text)
        {
            var range1 = SectionRange.FromText(range1Text);
            var range2 = SectionRange.FromText(range2Text);

            return range1.FullyContains(range2) || range2.FullyContains(range1);
        }

        [Test]
        [TestCase("2-4", "6-8", ExpectedResult = false)]
        [TestCase("2-3", "4-5", ExpectedResult = false)]
        [TestCase("5-7", "7-9", ExpectedResult = true)]
        [TestCase("2-8", "3-7", ExpectedResult = true)]
        [TestCase("6-6", "4-6", ExpectedResult = true)]
        [TestCase("2-6", "4-8", ExpectedResult = true)]
        public bool TestRangeOverlaps(string range1Text, string range2Text)
        {
            var range1 = SectionRange.FromText(range1Text);
            var range2 = SectionRange.FromText(range2Text);

            return range1.OverlapsWith(range2);
        }
    }
}
