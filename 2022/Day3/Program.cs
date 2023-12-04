namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rucksacksContents = File.ReadAllLines("input.txt");
            int sum = 0;
            foreach (var rucksackContent in rucksacksContents)
            {
                var rucksack = new Rucksack(rucksackContent);
                sum += rucksack.Priority;
            }

            Console.WriteLine($"Result 1: {sum}");

            var elfGroups = rucksacksContents
                            .Select((contents, index) => new { Rucksack = new Rucksack(contents), GroupNumber = index / 3 })
                            .GroupBy(x => x.GroupNumber)
                            .Select(g => new ElfGroup(g.ElementAt(0).Rucksack, g.ElementAt(1).Rucksack, g.ElementAt(2).Rucksack));
            int sum2 = elfGroups.Sum(e => Rucksack.GetItemPriority(e.FindGroupBadge()));
            
            Console.WriteLine($"Result 1: {sum2}");
        }
    }
}