using NUnit.Framework;

namespace Day15
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("rn=1", ExpectedResult = 30)]
        [TestCase("cm-", ExpectedResult = 253)]
        [TestCase("qp=3", ExpectedResult = 97)]
        [TestCase("cm=2", ExpectedResult = 47)]
        [TestCase("qp-", ExpectedResult = 14)]
        [TestCase("pc=4", ExpectedResult = 180)]
        [TestCase("ot=9", ExpectedResult = 9)]
        [TestCase("ab=5", ExpectedResult = 197)]
        [TestCase("pc-", ExpectedResult = 48)]
        [TestCase("pc=6", ExpectedResult = 214)]
        [TestCase("ot=7", ExpectedResult = 231)]
        public int Test_Hash(string input)
        {
            return Program.GetHash(input);
        }

        [Test]
        public void Test_InitSequence()
        {
            var initSeq = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
            Assert.That(Program.InitSequence(initSeq), Is.EqualTo(145));
        }
    }
}
