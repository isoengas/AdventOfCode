using NUnit.Framework;
using System.Drawing;

namespace Day3
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test_symbol_extraction()
        {
            string[] lines = [
                "467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...$.*....",
                ".664.598.."
            ];
            var expectedSymbols = new List<Symbol>
            {
                new(new(3, 1), '*'),
                new(new(6, 3), '#'),
                new(new(3, 4), '*'),
                new(new(5, 5), '+'),
                new(new(3, 8), '$'),
                new(new(5, 8), '*')
            };
            var symbols = Program.ExtractSymbols(lines).ToList();
            Assert.That(symbols, Is.EqualTo(expectedSymbols));
        }

        [Test]
        public void Test_part_extraction()
        {
            string[] lines = [
                "467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...$.*....",
                ".664.598.."
            ];
            List<int> expectedParts = [467, 35, 633, 617, 592, 755, 664, 598];
            var parts = Program.ExtractParts(lines);
            Assert.That(parts, Is.EqualTo(expectedParts));
        }

        [Test]
        public void Test_gear_extraction()
        {
            string[] lines = [
                "467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...$.*....",
                ".664.598.."
            ];
            List<int> expectedGears = [16345, 451490];
            var gears = Program.ExtractGears(lines);
            Assert.That(gears, Is.EqualTo(expectedGears));
        }
    }
}
