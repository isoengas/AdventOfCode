
using NUnit.Framework;

namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 2: Cube Conundrum");
            var lines = File.ReadAllLines("input.txt");
            var config = new Configuration(12, 13, 14);
            var possibleGames = lines.Select(Game.FromRecord).Where(g => g.IsPossible(config));
            Console.WriteLine("Part 1: " + possibleGames.Sum(g => g.GameId));
            var minConfigs = lines.Select(Game.FromRecord).Select(g => g.GetMinConfiguration());
            Console.WriteLine("Part 2: " + minConfigs.Sum(c => c.Power));
        }
    }

    internal class Game(int id)
    {
        public int GameId { get; } = id;
        public List<Configuration> Configurations { get; } = [];

        internal static Game FromRecord(string gameRecord)
        {
            var id = int.Parse(gameRecord.Split(':').First().Replace("Game ", ""));
            var game = new Game(id);
            var configs = gameRecord.Split(':').Last().Trim().Split(';');
            game.Configurations.AddRange(configs.Select(ParseConfig));
            return game;
        }

        public Configuration GetMinConfiguration()
        {
            var redProbe = new Configuration(0, int.MaxValue, int.MaxValue);
            while (!IsPossible(redProbe))
                redProbe = new Configuration(redProbe.RedCubes + 1, int.MaxValue, int.MaxValue);

            var greenProbe = new Configuration(int.MaxValue, 0, int.MaxValue);
            while (!IsPossible(greenProbe))
                greenProbe = new Configuration(int.MaxValue, greenProbe.GreenCubes + 1, int.MaxValue);

            var blueProbe = new Configuration(int.MaxValue, int.MaxValue, 0);
            while (!IsPossible(blueProbe))
                blueProbe = new Configuration(int.MaxValue, int.MaxValue, blueProbe.BlueCubes + 1);

            return new Configuration(redProbe.RedCubes, greenProbe.GreenCubes, blueProbe.BlueCubes);
        }

        internal bool IsPossible(Configuration realConfiguration)
        {
            return Configurations.All(c => c.IsPossible(realConfiguration));
        }

        private static Configuration ParseConfig(string configRecord)
        {
            var colorRecords = configRecord.Split(',');
            int red = 0;
            int blue = 0;
            int green = 0;
            foreach (var colorRecord in colorRecords) 
            {
                var count = int.Parse(colorRecord.Trim().Split(' ').First());
                switch (colorRecord.Split(' ').Last())
                {
                    case "red":
                        red = count;
                        break;
                    case "blue":
                        blue = count;
                        break;
                    case "green":
                        green = count;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            return new Configuration(red, green, blue);
        }
    }

    internal class Configuration : IEquatable<Configuration>
    {
        public Configuration(int red, int green, int blue)
        {
            RedCubes = red;
            GreenCubes = green;
            BlueCubes = blue;
        }

        public int RedCubes { get; }
        public int BlueCubes { get; }
        public int GreenCubes { get; }

        public int Power => RedCubes * GreenCubes * BlueCubes;

        public bool Equals(Configuration other)
        {
            return other.BlueCubes == BlueCubes && other.GreenCubes == GreenCubes && other.RedCubes == RedCubes;
        }

        internal bool IsPossible(Configuration realConfiguration)
        {
            return realConfiguration.RedCubes >= RedCubes && realConfiguration.GreenCubes >= GreenCubes && realConfiguration.BlueCubes >= BlueCubes;
        }
    }
}
