namespace Day3
{
    internal class Rucksack
    {
        private readonly string _compartment1;
        private readonly string _compartment2;

        public Rucksack(string contents)
        {
            _compartment1 = contents.Substring(0, contents.Length / 2);
            _compartment2 = contents.Substring(contents.Length / 2);
        }

        public int Priority
        {
            get
            {
                var misplaced = _compartment1.Distinct().Single(item => _compartment2.Contains(item));
                return GetItemPriority(misplaced);
            }
        }

        public IEnumerable<char> Items => _compartment1 + _compartment2;

        internal static int GetItemPriority(char item)
        {
            if (item >= 'a' && item <= 'z')
            {
                return item - 'a' + 1;
            }
            else
            {
                return item - 'A' + 27;
            }
        }
    }
}