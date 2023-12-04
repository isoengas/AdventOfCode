using NUnit.Framework;

namespace Day7
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void TestExampleDirectory()
        {
            var shell = new Shell();
            shell.AddDirectory("a");
            shell.AddFile("b.txt", 14848514);
            shell.AddFile("c.dat", 8504156);
            shell.AddDirectory("d");
            shell.Cd("a");
            shell.AddDirectory("e");
            shell.AddFile("f", 29116);
            shell.AddFile("g", 2557);
            shell.AddFile("h.lst", 62596);
            shell.Cd("e");
            shell.AddFile("i", 584);
            shell.CdParent();
            shell.CdParent();
            shell.Cd("d");
            shell.AddFile("j", 4060174);
            shell.AddFile("d.log", 8033020);
            shell.AddFile("d.ext", 5626152);
            shell.AddFile("k", 7214296);

            shell.GotoRoot();
            var sizeRoot = shell.CurrentDirectory.GetTotalSize();
            Assert.That(sizeRoot, Is.EqualTo(48381165));
            shell.Cd("a");
            var sizeA = shell.CurrentDirectory.GetTotalSize();
            Assert.That(sizeA, Is.EqualTo(94853));
        }

        [Test]
        public void TestShellParser()
        {
            var shell = new Shell();
            var shellParser = new ShellParser(shell);
            shellParser.ParseLine("$ cd /");
            shellParser.ParseLine("$ ls");
            shellParser.ParseLine("dir a");
            shellParser.ParseLine("14848514 b.txt");
            shellParser.ParseLine("8504156 c.dat");
            shellParser.ParseLine("dir d");
            shellParser.ParseLine("$ cd a");
            shellParser.ParseLine("$ ls");
            shellParser.ParseLine("dir e");
            shellParser.ParseLine("29116 f");
            shellParser.ParseLine("2557 g");
            shellParser.ParseLine("62596 h.lst");
            shellParser.ParseLine("$ cd e");
            shellParser.ParseLine("$ ls");
            shellParser.ParseLine("584 i");
            shellParser.ParseLine("$ cd ..");
            shellParser.ParseLine("$ cd ..");
            shellParser.ParseLine("$ cd d");
            shellParser.ParseLine("$ ls");
            shellParser.ParseLine("4060174 j");
            shellParser.ParseLine("8033020 d.log");
            shellParser.ParseLine("5626152 d.ext");
            shellParser.ParseLine("7214296 k");

            shell.GotoRoot();
            var sizeRoot = shell.CurrentDirectory.GetTotalSize();
            Assert.That(sizeRoot, Is.EqualTo(48381165));
            shell.Cd("a");
            var sizeA = shell.CurrentDirectory.GetTotalSize();
            Assert.That(sizeA, Is.EqualTo(94853));

            shell.GotoRoot();
            var dirsWithAtMost100000 = shell.CurrentDirectory.Walk().Where(d => d.GetTotalSize() <= 100000);
            Assert.That(dirsWithAtMost100000, Has.Exactly(2).Items);
            var sum = dirsWithAtMost100000.Sum(d => d.GetTotalSize());
            Assert.That(sum, Is.EqualTo(95437));
        }
    }
}
