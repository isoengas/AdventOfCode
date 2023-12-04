using NUnit.Framework;

namespace Day5
{

    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void GetTopShouldWork()
        {
            var sut = new Ship(new[]
            {
                new[] { 'a' },
                new[] { 'b' },
                new[] { 'c' }
            });
            var result = sut.GetTop();
            Assert.That(result, Is.EquivalentTo(new[] { 'a', 'b', 'c' }));
        }

        [Test]
        public void GetTopShouldWorkWithMoreThanOneLevel()
        {
            var sut = new Ship(new[]
            {
                new[] { 'a', 'z' },
                new[] { 'b' },
                new[] { 'c', 'd' }
            });
            var result = sut.GetTop();
            Assert.That(result, Is.EquivalentTo(new[] { 'z', 'b', 'd' }));
        }

        [Test]
        public void GetTopShouldWorkWithEmptyStack()
        {
            var sut = new Ship(new[]
            {
                new[] { 'a', 'z' },
                new char[] {  },
                new[] { 'c', 'd' }
            });
            var result = sut.GetTop();
            Assert.That(result, Is.EquivalentTo(new[] { 'z', ' ', 'd' }));
        }

        [Test]
        public void MoveShouldWork()
        {
            var sut = new Ship(new[]
            {
                new[] { 'a', 'z' },
                new char[] {  },
                new[] { 'c', 'd' }
            });
            sut.Move(1, 1, 2); // Move 1 from stack 1 to stack 2
            var result = sut.GetTop();
            Assert.That(result, Is.EquivalentTo(new[] { 'a', 'z', 'd' }));
        }

        [Test]
        public void MoveSeveralShouldWork()
        {
            var sut = new Ship(new[]
            {
                new[] { 'a', 'z' },
                new char[] {  },
                new[] { 'c', 'd' }
            });
            sut.Move(2, 1, 3); // Move 2 from stack 1 to stack 3
            var result = sut.GetTop();
            Assert.That(result, Is.EquivalentTo(new[] { ' ', ' ', 'a' }));
        }

        [Test]
        public void MovementParseWorks()
        {
            var movementText = "move 5 from 4 to 9";
            var movement = Movement.Parse(movementText);
            Assert.That(movement.NumCrates, Is.EqualTo(5));
            Assert.That(movement.FromStack, Is.EqualTo(4));
            Assert.That(movement.ToStack, Is.EqualTo(9));
        }

        [Test]
        public void MoveMultipleShouldWork()
        {
            var sut = new Ship(new[]
            {
                new[] { 'z', 'n', 'd' },
                new char[] { 'm', 'c'  },
                new[] { 'p', }
            });
            sut.MoveMultiple(3, 1, 3); // Move 2 from stack 1 to stack 3
            var result = sut.GetTop();
            Assert.That(result, Is.EquivalentTo(new[] { ' ', 'c', 'd' }));
        }
    }
}
