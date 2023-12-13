using NUnit.Framework;


namespace Day8
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test_example_set_1()
        {
            string directions = "RL";
            var nodes = new Dictionary<string, Node>
            {
                ["AAA"] = new("BBB", "CCC"),
                ["BBB"] = new("DDD", "EEE"),
                ["CCC"] = new("ZZZ", "GGG"),
                ["DDD"] = new("DDD", "DDD"),
                ["EEE"] = new("EEE", "EEE"),
                ["GGG"] = new("GGG", "GGG"),
                ["ZZZ"] = new("ZZZ", "ZZZ"),
            };
            var numSteps = Program.GetNumSteps(directions, nodes);
            Assert.That(numSteps, Is.EqualTo(2));
        }

        [Test]
        public void Test_example_set_2()
        {
            string directions = "LLR";
            var nodes = new Dictionary<string, Node>
            {
                ["AAA"] = new("BBB", "BBB"),
                ["BBB"] = new("AAA", "ZZZ"),
                ["ZZZ"] = new("ZZZ", "ZZZ")
            };
            var numSteps = Program.GetNumSteps(directions, nodes);
            Assert.That(numSteps, Is.EqualTo(6));
        }

        [Test]
        public void Test_example_set_3()
        {
            string directions = "LR";
            var nodes = new Dictionary<string, Node>
            {
                ["11A"] = new("11B", "XXX"),
                ["11B"] = new("XXX", "11Z"),
                ["11Z"] = new("11B", "XXX"),
                ["22A"] = new("22B", "XXX"),
                ["22B"] = new("22C", "22C"),
                ["22C"] = new("22Z", "22Z"),
                ["22Z"] = new("22B", "22B"),
                ["XXX"] = new("XXX", "CCC"),
            };
            var numSteps = Program.GetNumStepsForGhost(directions, nodes);
            Assert.That(numSteps, Is.EqualTo(6));
        }
    }
}
