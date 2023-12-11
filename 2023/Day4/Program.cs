namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 4: Scratchcards");
            var input = File.ReadAllLines("input.txt");
            var score = input.Select(i => new Card(i)).Select(c => c.GetPoints()).Sum();
            Console.WriteLine("Part 1: " + score);
            var cards = input.Select(i => new Card(i)).ToArray();
            ProcessCards(cards);

            var totalCards = cards.Sum(c => c.NumInstances);
            Console.WriteLine("Part 2: " + totalCards);
        }

        internal static void ProcessCards(Card[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                var wins = cards[i].GetMatchingNumbersCount();
                for (int j = 1; j <= wins; j++)
                {
                    cards[i + j].NumInstances += cards[i].NumInstances;
                }
            }
        }
    }
}
