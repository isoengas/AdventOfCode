using NUnit.Framework;

namespace Day3
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", ExpectedResult = 16)]
        [TestCase("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", ExpectedResult = 38)]
        [TestCase("PmmdzqPrVvPwwTWBwg", ExpectedResult = 42)]
        [TestCase("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", ExpectedResult = 22)]
        [TestCase("ttgJtRGJQctTZtZT", ExpectedResult = 20)]
        [TestCase("CrZsJsPPZsGzwwsLwLmpwMDw", ExpectedResult = 19)]
        public int TestPriorityWorks(string contents)
        {
            var rucksack = new Rucksack(contents);
            return rucksack.Priority;
        }

        [Test]
        [TestCase("vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg", ExpectedResult = 18)]
        [TestCase("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "ttgJtRGJQctTZtZT", "CrZsJsPPZsGzwwsLwLmpwMDw", ExpectedResult = 52)]
        public int TestBadgeSearchWorks(string rucksack1, string rucksack2, string rucksack3)
        {
            var elfGroup = new ElfGroup(new Rucksack(rucksack1), new Rucksack(rucksack2), new Rucksack(rucksack3));
            return Rucksack.GetItemPriority(elfGroup.FindGroupBadge());
        }
    }
}
