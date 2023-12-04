namespace Day2
{
    public class Game
    {
        private readonly char _me;
        private readonly char _opponent;
        public Game(char opponent, char me)
        {
            _opponent = opponent;
            _me = me;
        }

        public static Game FromResult(char opponent, char result)
        {
            switch (result)
            {
                case 'X': // Lose
                    return new Game(opponent, GetLosingMove(opponent));
                case 'Y': // Draw
                    return new Game(opponent, GetDrawMove(opponent));
                default: // Win
                    return new Game(opponent, GetWinningMove(opponent));
            }
        }

        private static char GetLosingMove(char opponent) => opponent switch
        {
            'A' => 'Z',
            'B' => 'X',
            'C' => 'Y',
            _ => 'X'
        };

        private static char GetDrawMove(char opponent) => opponent switch
        {
            'A' => 'X',
            'B' => 'Y',
            'C' => 'Z',
            _ => 'X'
        };

        private static char GetWinningMove(char opponent) => opponent switch

        {
            'A' => 'Y',
            'B' => 'Z',
            'C' => 'X',
            _ => 'X'
        };

        public int TotalScore => GetShapeScore + GetOutcomeScore;

        private int GetShapeScore => _me switch
        {
            'X' => 1,
            'Y' => 2,
            'Z' => 3,
            _ => 0
        };

        private int GetOutcomeScore => _me switch
        {
            'X' when _opponent == 'A' => 3,
            'Y' when _opponent == 'B' => 3,
            'Z' when _opponent == 'C' => 3,
            'X' when _opponent == 'C' => 6,
            'Y' when _opponent == 'A' => 6,
            'Z' when _opponent == 'B' => 6,
            _ => 0
        };
    }
}
