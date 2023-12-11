namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 7: Camel Cards");
            
            var gameRecords = File.ReadLines("input.txt");
            
            var games = gameRecords
                            .Select(r => new Game(new Hand(r.Split(' ')[0]), int.Parse(r.Split(' ')[1])));
            var winnings = games.OrderBy(h => h.hand).Select((game, index) => (long)(index + 1) * game.bid).Sum();
            Console.WriteLine("Part 1: " + winnings);
            
            var gamesWithJoker = gameRecords
                            .Select(r => new GameWithJoker(new HandWithJoker(r.Split(' ')[0]), int.Parse(r.Split(' ')[1])));
            winnings = gamesWithJoker.OrderBy(h => h.hand).Select((game, index) => (long)(index + 1) * game.bid).Sum();
            Console.WriteLine("Part 2: " + winnings);
        }
    }

    public record Hand(string cards) : IComparable<Hand>
    {
        private const string Strength = "23456789TJQKA";

        public string Cards => cards;

        public int GetHandWeight()
        {
            return cards
                    .GroupBy(c => c)
                    .Select(g => g.Count())
                    .Where(c => c > 1)
                    .OrderByDescending(c => c)
                    .Select((num, index) => new { num, index })
                    .Aggregate(0, (acc, numIndex) => (int)(acc + (numIndex.num * Math.Pow(10, (1 - numIndex.index)))));
        }

        public int CompareTo(Hand other)
        {
            int xWeight = GetHandWeight();
            int yWeight = other.GetHandWeight();
            if (xWeight != yWeight) return xWeight - yWeight;
            int i = 0;
            while (i < 5)
            {
                int strX = Strength.IndexOf(Cards[i]);
                int strY = Strength.IndexOf(other.Cards[i]);
                if (strX != strY) return strX - strY;
                i++;
            }
            return 0;
        }
    }

    public record HandWithJoker(string cards) : IComparable<HandWithJoker>
    {
        private const string Strength = "J23456789TQKA";

        public string Cards => cards;

        public int GetHandWeight()
        {
            var weightWithoutJokers = Cards
                    .Where(c => c != 'J')
                    .GroupBy(c => c)
                    .Select(g => g.Count())
                    .Where(c => c > 1)
                    .OrderByDescending(c => c)
                    .Select((num, index) => new { num, index })
                    .Aggregate(0, (acc, numIndex) => (int)(acc + (numIndex.num * Math.Pow(10, (1 - numIndex.index)))));
            var numJokers = Cards.Count(c => c == 'J');
            return numJokers switch
            {
                0 => weightWithoutJokers,
                1 => weightWithoutJokers > 0 ? weightWithoutJokers + 10 : 20,
                2 => weightWithoutJokers > 0 ? weightWithoutJokers + 20 : 30,
                3 => weightWithoutJokers > 0 ? 50 : 40,
                4 => 50,
                5 => 50
            };
        }

        public int CompareTo(HandWithJoker other)
        {
            int xWeight = GetHandWeight();
            int yWeight = other.GetHandWeight();
            if (xWeight != yWeight) return xWeight - yWeight;
            int i = 0;
            while (i < 5)
            {
                int strX = Strength.IndexOf(Cards[i]);
                int strY = Strength.IndexOf(other.Cards[i]);
                if (strX != strY) return strX - strY;
                i++;
            }
            return 0;
        }
    }

    public record Game(Hand hand, int bid);
    public record GameWithJoker(HandWithJoker hand, int bid);
}
