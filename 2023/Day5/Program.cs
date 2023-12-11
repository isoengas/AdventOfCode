namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 5: If You Give A Seed A Fertilizer");
            var lines = File.ReadAllLines("input.txt");
            long[] seeds = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var seedToSoil = BuildMapper(3, 24, lines);
            var soilToFertilizer = BuildMapper(29, 31, lines);
            var fertilizerToWater = BuildMapper(62, 10, lines);
            var waterToLight = BuildMapper(74, 27, lines);
            var lightToTemperature = BuildMapper(103, 11, lines);
            var temperatureToHumidity = BuildMapper(116, 13, lines);
            var humidityToLocation = BuildMapper(131, 8, lines);

            long MapSeed(long seed)
            {
                return humidityToLocation.Map(
                    temperatureToHumidity.Map(
                        lightToTemperature.Map(
                            waterToLight.Map(
                                fertilizerToWater.Map(
                                    soilToFertilizer.Map(
                                        seedToSoil.Map(seed)
                                        )
                                    )
                                )
                            )
                        )
                    );
            }

            var minLocation = seeds.Select(MapSeed).Min();
            Console.Write("Part 1: " + minLocation);

            IEnumerable<long> seedsInRange = seeds.Zip(seeds.Skip(1), Tuple.Create)
                .Select(t => CreateRange(t.Item1, t.Item2))
                .SelectMany(t => t);
            var minLocation2 = seedsInRange.Select(MapSeed).Min();
            Console.Write("Part 2: " + minLocation2);
        }

        public static IEnumerable<long> CreateRange(long start, long count)
        {
            var limit = start + count;

            while (start < limit)
            {
                yield return start;
                start++;
            }
        }

        private static Mapper BuildMapper(int fromLine, int numLines, string[] lines)
        {
            return new Mapper(
                    Enumerable.Range(fromLine, numLines)
                        .Select(l => lines[l].Split(' ').Select(long.Parse).ToArray())
                        .Select(r => new MapperRange(r[0], r[1], r[2]))
                        .ToArray()
                );
        }
    }

    internal class Mapper(MapperRange[] mapperRanges)
    {
        public long Map(long source)
        {
            return mapperRanges
                .Select(m => m.Map(source))
                .Where(m => m.HasValue)
                .FirstOrDefault()
                .GetValueOrDefault(source);
        }
    }

    internal class MapperRange
    {
        private readonly long _destinationStart;
        private readonly long _sourceStart;
        private readonly long _length;

        public MapperRange(long destinationStart, long sourceStart, long length)
        {
            _destinationStart = destinationStart;
            _sourceStart = sourceStart;
            _length = length;
        }

        public long? Map(long source)
        {
            if (source >= _sourceStart && source <= _sourceStart + _length)
                return _destinationStart + source - _sourceStart;
            return null;
        }
    }
}
