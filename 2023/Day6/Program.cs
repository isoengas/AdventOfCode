namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 6: Wait For It");
            var race1 = new Race(45, 295);
            var race2 = new Race(98, 1734);
            var race3 = new Race(83, 1278);
            var race4 = new Race(73, 1210);
            Console.WriteLine("Part 1: " + race1.WinningMoves() * race2.WinningMoves() * race3.WinningMoves() * race4.WinningMoves());
            var moves = new Race(45988373, 295173412781210).WinningMoves();
            Console.WriteLine("Part 2: " + moves);
        }

        internal class Race(long time, long distance)
        {
            public bool IsWinner(long pushTime)
            {
                long d = (time - pushTime) * pushTime;
                return d > distance;
            }

            public int WinningMoves()
            {
                int count = 0;
                bool winnersStarted = false;
                for (long i = 1; i <= time; i++)
                {
                    if (IsWinner(i))
                    {
                        winnersStarted = true;
                        count++;
                    }
                    else if (winnersStarted)
                    {
                        return count;
                    }
                }
                return count;
            }
        }
    }
}
