namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");
            var shell = new Shell();
            var shellParser = new ShellParser(shell);
            foreach (var line in input)
            {
                shellParser.ParseLine(line);
            }
            shell.GotoRoot();
            var result = shell
                            .CurrentDirectory
                            .Walk()
                            .Select(d => d.GetTotalSize())
                            .Where(size => size <= 100000)
                            .Sum();
            Console.WriteLine($"The total sum of directories with at most 100000 size is {result}");

            var totalSize = shell.CurrentDirectory.GetTotalSize();
            var availableSize = 70000000 - totalSize;
            var needToFreeUp = 30000000 - availableSize;
            if (needToFreeUp <= 0)
            {
                Console.WriteLine("Enough space for the update!");
            }
            else
            {
                var smallestDir = shell
                                    .CurrentDirectory
                                    .Walk()
                                    .Select(d => d.GetTotalSize())
                                    .Where(size => size >= needToFreeUp)
                                    .OrderBy(size => size)
                                    .First();
                Console.WriteLine($"The size of the smallest directory to delete is {smallestDir}");
            }
        }
    }
}