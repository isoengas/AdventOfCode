using NUnit.Framework;
using System.Text.Json.Nodes;

namespace Day13
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        [TestCase("[1,1,3,1,1]")]
        [TestCase("[[1],[2,3,4]]")]
        public void TestParseAsJson(string input)
        {
            var array = JsonNode.Parse(input)!.AsArray();
            Assert.That(array, Is.Not.Null);
        }

        [Test]
        [TestCase("[1,1,3,1,1]", "[1,1,5,1,1]", ExpectedResult = -1)]
        [TestCase("[[1],[2,3,4]]", "[[1],4]", ExpectedResult = -1)]
        [TestCase("[9]", "[[8,7,6]]", ExpectedResult = 1)]
        [TestCase("[[4,4],4,4]", "[[4,4],4,4,4]", ExpectedResult = -1)]
        [TestCase("[7,7,7,7]", "[7,7,7]", ExpectedResult = 1)]
        [TestCase("[]", "[3]", ExpectedResult = -1)]
        [TestCase("[[[]]]", "[[]]", ExpectedResult = 1)]
        [TestCase("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", ExpectedResult = 1)]
        public int TestIsPairOrdered(string input1, string input2)
        {
            var array1 = JsonNode.Parse(input1)!.AsArray();
            var array2 = JsonNode.Parse(input2)!.AsArray();
            return new PacketComparer().Compare(array1, array2);
        }
    }
}
