using NUnit.Framework;

namespace Day8
{
    [TestFixture]
    public class UnitTests
    {
        private readonly Grid _grid = new Grid(5);
        public UnitTests()
        {
            _grid.AddRow(new[] { 3, 0, 3, 7, 3 });
            _grid.AddRow(new[] { 2, 5, 5, 1, 2 });
            _grid.AddRow(new[] { 6, 5, 3, 3, 2 });
            _grid.AddRow(new[] { 3, 3, 5, 4, 9 });
            _grid.AddRow(new[] { 3, 5, 3, 9, 0 });
        }

        [Test]
        public void TestGrid()
        {
            Assert.That(_grid.IsItemVisible(1, 2), Is.EqualTo(true));

            var numVisible = _grid.EnumerateItems().Count(item => _grid.IsItemVisible(item.x, item.y));

            Assert.That(numVisible, Is.EqualTo(21));
        }

        [Test]
        public void TestScenicScore()
        {
            Assert.That(_grid.ScenicScore(2, 1), Is.EqualTo(4));
            Assert.That(_grid.ScenicScore(2, 3), Is.EqualTo(8));

            var maxScore = _grid.EnumerateItems().Max(item => _grid.ScenicScore(item.x, item.y));
            Assert.That(maxScore, Is.EqualTo(8));
        }

    }
}
