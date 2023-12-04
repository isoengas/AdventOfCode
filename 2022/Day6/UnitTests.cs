using NUnit.Framework;

namespace Day6
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 4, ExpectedResult = 7)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 4, ExpectedResult = 5)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 4, ExpectedResult = 6)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 4, ExpectedResult = 10)]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 4, ExpectedResult = 11)]
        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 14, ExpectedResult = 19)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 14, ExpectedResult = 23)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 14, ExpectedResult = 23)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 14, ExpectedResult = 29)]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 14, ExpectedResult = 26)]
        public int TestMarkerDetection(string buffer, int markerLength)
        {
            var markerDetector = new MarkerDetector(markerLength);
            return markerDetector.GetFirstMarkerPosition(buffer);
        }

    }
}
