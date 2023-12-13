

namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 9: Mirage Maintenance");
            var lines = File.ReadAllLines("input.txt");
            var result = lines.Select(Parse).Sum(Extrapolate);
            Console.WriteLine("Part 1: " + result);
            result = lines.Select(Parse).Select(seq => seq.Reverse().ToArray()).Sum(Extrapolate);
            Console.WriteLine("Part 2: " + result);
        }

        public static int[] Parse(string line)
        {
            return line.Split(' ').Select(int.Parse).ToArray();
        }

        public static int Extrapolate(int[] sequence)
        {
            if (sequence.All(x => x == 0)) return 0;
            return sequence.Last() + Extrapolate(Enumerable.Range(1, sequence.Length - 1).Select(i => sequence[i] - sequence[i - 1]).ToArray());
        }
    }
}
