namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var cpu = new Cpu(lines.Select(l => InstructionParser.Parse(l)).ToList());
            int sum = cpu.Ticks(20) * 20 +
                cpu.Ticks(40) * 60 +
                cpu.Ticks(40) * 100 +
                cpu.Ticks(40) * 140 +
                cpu.Ticks(40) * 180 +
                cpu.Ticks(40) * 220;
            Console.WriteLine("Day 10 - Cathode-Ray Tube");
            Console.WriteLine($"Part 1, sum of signal strenghts is {sum}");

            Console.WriteLine("Part 2, render letters:");
            Console.WriteLine();
            var crt = new Crt(new Cpu(lines.Select(l => InstructionParser.Parse(l)).ToList()));
            crt.Render();
            Console.WriteLine();
        }
    }
}