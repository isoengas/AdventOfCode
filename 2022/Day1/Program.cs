namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var elfs = new List<Elf>();
            var currentElf = new Elf();
            elfs.Add(currentElf);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    currentElf = new Elf();
                    elfs.Add(currentElf);
                    continue;
                }
                else
                {
                    var callories = int.Parse(line);
                    currentElf.AddCallories(callories);
                }
            }
            var elf3WithMostCallories = elfs
                                            .OrderByDescending(e => e.TotalCallories)
                                            .Take(3)
                                            .Sum(e => e.TotalCallories);
            Console.WriteLine($"Result: {elf3WithMostCallories}");
        }
    }
}