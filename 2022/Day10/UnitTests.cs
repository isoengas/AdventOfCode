using NUnit.Framework;

namespace Day10
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void TestInitialState()
        {
            var cpu = new Cpu(new List<Instruction>());
            Assert.That(cpu.X, Is.EqualTo(1));
        }

        [Test]
        public void TestFirstCycles()
        {
            var cpu = new Cpu(new List<Instruction> { Instruction.Noop() });
            cpu.Ticks(1);
            Assert.That(cpu.X, Is.EqualTo(1));
            cpu.Ticks(1);
            Assert.That(cpu.X, Is.EqualTo(1));
        }

        [Test]
        public void TestSecondInstruction()
        {
            var cpu = new Cpu(new List<Instruction> { Instruction.Noop(), Instruction.Addx(3)});
            cpu.Ticks(3);
            Assert.That(cpu.X, Is.EqualTo(4));
        }

        [Test]
        [TestCase(20, ExpectedResult = 420)]
        [TestCase(60, ExpectedResult = 1140)]
        [TestCase(100, ExpectedResult = 1800)]
        [TestCase(140, ExpectedResult = 2940)]
        [TestCase(180, ExpectedResult = 2880)]
        [TestCase(220, ExpectedResult = 3960)]

        public int TestExampleInput(int ticks)
        {
            var lines = File.ReadAllLines("exampleInput.txt");
            var cpu = new Cpu(lines.Select(l => InstructionParser.Parse(l)).ToList());
            return cpu.Ticks(ticks) * ticks;
        }
    }
}
