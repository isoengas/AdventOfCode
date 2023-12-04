namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var movesLines = File.ReadAllLines("input.txt");
            int totalScore = 0;
            foreach (var line in movesLines)
            {
                var opponent = line[0];
                var result = line[2];
                totalScore += Game.FromResult(opponent, result).TotalScore;
            }
            Console.WriteLine($"Result: {totalScore}");
        }
    }
}