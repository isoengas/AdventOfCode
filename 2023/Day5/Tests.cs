using NUnit.Framework;

namespace Day5
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(98, ExpectedResult = 50)]
        [TestCase(99, ExpectedResult = 51)]
        [TestCase(53, ExpectedResult = 55)]
        [TestCase(10, ExpectedResult = 10)]
        public long TestSingleMapper(long seed)
        {
            MapperRange[] mapRanges = [
                new(50, 98, 2),
                new(52, 50, 48)
            ];
            var mapper = new Mapper(mapRanges);
            return mapper.Map(seed);
        }

        [Test]
        [TestCase(79, ExpectedResult = 82)]
        [TestCase(14, ExpectedResult = 43)]
        [TestCase(55, ExpectedResult = 86)]
        [TestCase(13, ExpectedResult = 35)]
        public long TestMultiMappers(long seed)
        {
            MapperRange[] mapRanges1 = [
                new(50, 98, 2),
                new(52, 50, 48)
            ];
            var mapper1 = new Mapper(mapRanges1);

            MapperRange[] mapRanges2 = [
                new(0, 15, 37),
                new(37, 52, 2),
                new(39, 0, 15)
            ];
            var mapper2 = new Mapper(mapRanges2);

            MapperRange[] mapRanges3 = [
                new(49, 53, 8),
                new(0, 11, 42),
                new(42, 0, 7)
            ];
            var mapper3 = new Mapper(mapRanges3);

            MapperRange[] mapRanges4 = [
                new(88, 18, 7),
                new(18, 25, 70)
            ];
            var mapper4 = new Mapper(mapRanges4);

            MapperRange[] mapRanges5 = [
                new(45, 77, 23),
                new(81, 45, 19),
                new(68, 64, 13)
            ];
            var mapper5 = new Mapper(mapRanges5);

            MapperRange[] mapRanges6 = [
                new(0, 69, 1),
                new(1, 0, 69)
            ];
            var mapper6 = new Mapper(mapRanges6);

            MapperRange[] mapRanges7 = [
                new(60, 56, 37),
                new(56, 93, 4)
            ];
            var mapper7 = new Mapper(mapRanges7);

            return mapper7.Map(
                mapper6.Map(
                    mapper5.Map(
                        mapper4.Map(
                            mapper3.Map(
                                mapper2.Map(
                                    mapper1.Map(seed)
                                    )
                                )
                            )
                        )
                    )
                );
        }
    }
}
