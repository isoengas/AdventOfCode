using NUnit.Framework;

namespace Day12
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void TestPart1ExampleInput()
        {
            char[,] map =
            {
                { 'S','a','b','q','p','o','n','m' },
                { 'a','b','c','r','y','x','x','l' },
                { 'a','c','c','s','z','E','x','k' },
                { 'a','c','c','t','u','v','w','j' },
                { 'a','b','d','e','f','g','h','i' }
            };
            var result = Dijkstra.FindShortestRoute(map, (0, 0), (2, 5));
            Assert.That(result, Has.Exactly(32).Items); // 31 steps = 32 nodes
        }

        [Test]
        public void TestPart2ExampleInput()
        {
            char[,] map =
            {
                { 'S','a','b','q','p','o','n','m' },
                { 'a','b','c','r','y','x','x','l' },
                { 'a','c','c','s','z','E','x','k' },
                { 'a','c','c','t','u','v','w','j' },
                { 'a','b','d','e','f','g','h','i' }
            };
            var result = Dijkstra.FindNearestNode(map, (2, 5));
            Assert.That(result, Has.Exactly(30).Items); // 29 steps = 30 nodes
        }
    }
}
