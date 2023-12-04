namespace Day3
{
    internal class ElfGroup
    {
        private Rucksack _rucksack1;
        private Rucksack _rucksack2;
        private Rucksack _rucksack3;

        public ElfGroup(Rucksack rucksack1, Rucksack rucksack2, Rucksack rucksack3)
        {
            _rucksack1 = rucksack1;
            _rucksack2 = rucksack2;
            _rucksack3 = rucksack3;
        }

        public char FindGroupBadge()
        {
            return _rucksack1.Items
                    .Intersect(_rucksack2.Items)
                    .Intersect(_rucksack3.Items)
                    .Distinct()
                    .Single();
        }
    }
}