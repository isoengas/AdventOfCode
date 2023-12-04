using NUnit.Framework;
using System.IO;

namespace Day16
{
    [TestFixture]
    public class UnitTests
    {
        private readonly Volcano v;
        public UnitTests()
        {
            v = new Volcano
            {
                ["AA"] = new Valve("AA", 0),
                ["BB"] = new Valve("BB", 13),
                ["CC"] = new Valve("CC", 2),
                ["DD"] = new Valve("DD", 20),
                ["EE"] = new Valve("EE", 3),
                ["FF"] = new Valve("FF", 0),
                ["GG"] = new Valve("GG", 0),
                ["HH"] = new Valve("HH", 22),
                ["II"] = new Valve("II", 0),
                ["JJ"] = new Valve("JJ", 21)
            };
            v.AddTunnel("AA", "DD");
            v.AddTunnel("AA", "II");
            v.AddTunnel("AA", "BB");
            v.AddTunnel("BB", "CC");
            v.AddTunnel("BB", "AA");
            v.AddTunnel("CC", "DD");
            v.AddTunnel("CC", "BB");
            v.AddTunnel("DD", "CC");
            v.AddTunnel("DD", "AA");
            v.AddTunnel("DD", "EE");
            v.AddTunnel("EE", "FF");
            v.AddTunnel("EE", "DD");
            v.AddTunnel("FF", "EE");
            v.AddTunnel("FF", "GG");
            v.AddTunnel("GG", "FF");
            v.AddTunnel("GG", "HH");
            v.AddTunnel("HH", "GG");
            v.AddTunnel("II", "AA");
            v.AddTunnel("II", "JJ");
            v.AddTunnel("JJ", "II");
        }
        [Test]
        public void TestPathResult()
        {
            Step[] path =
            {
                new Move(v["DD"]),
                new OpenValve("DD"),
                new Move(v["CC"]),
                new Move(v["BB"]),
                new OpenValve("BB"),
                new Move(v["AA"]),
                new Move(v["II"]),
                new Move(v["JJ"]),
                new OpenValve("JJ"),
                new Move(v["II"]),
                new Move(v["AA"]),
                new Move(v["DD"]),
                new Move(v["EE"]),
                new Move(v["FF"]),
                new Move(v["GG"]),
                new Move(v["HH"]),
                new OpenValve("HH"),
                new Move(v["GG"]),
                new Move(v["FF"]),
                new Move(v["EE"]),
                new OpenValve("EE"),
                new Move(v["DD"]),
                new Move(v["CC"]),
                new OpenValve("CC"),
            };
            var pressureReleased = v.ComputePath(v["AA"], 30, path);
            Assert.That(pressureReleased, Is.EqualTo(1651));
        }

        [Test]
        public void TestComputePath()
        {
            var result = v.Compute(v["AA"], 30);
            v.Reset();
            var pressureReleased = v.ComputePath(v["AA"], 30, result.ToArray());
            Assert.That(result, Is.Not.Null);
        }

    }
}
