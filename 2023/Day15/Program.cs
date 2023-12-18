
namespace Day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 15: Lens Library");
            string line = File.ReadAllText("input.txt");
            var sum = line.Split(',').Select(GetHash).Sum();
            Console.WriteLine("Part 1: " + sum);
            sum = InitSequence(line);
            Console.WriteLine("Part 2: " + sum);
        }

        public static int InitSequence(string line)
        {
            var boxes = Enumerable.Range(1, 256).Select(i => new Box(i)).ToArray();
            foreach (var instr in line.Split(','))
            {
                if (instr.Contains('-'))
                    RemoveLens(boxes, instr.Split('-')[0]);
                else if (instr.Contains('='))
                    PutLens(boxes, instr.Split('=')[0], int.Parse(instr.Split('=')[1]));
            }
            return boxes.Select(b => b.FocusingPower).Sum();
        }

        private static void RemoveLens(Box[] boxes, string label)
        {
            int boxNumber = GetHash(label);
            var lens = boxes[boxNumber].GetLens(label);
            if (lens is not null)
            {
                boxes[boxNumber].Lenses.Remove(lens);
            }
        }

        private static void PutLens(Box[] boxes, string label, int focalLength)
        {
            int boxNumber = GetHash(label);
            var lens = boxes[boxNumber].GetLens(label);
            if (lens is not null)
            {
                lens.FocalLength = focalLength;
            }
            else
            {
                boxes[boxNumber].Lenses.Add(new Lens(label, focalLength));
            }
        }

        public static int GetHash(string s)
        {
            int curr = 0;
            foreach (var c in s)
            {
                curr += c;
                curr *= 17;
                curr %= 256;
            }
            return curr;
        }
    }

    public class Box(int boxNumber)
    {
        public int BoxNumber => boxNumber;
        public List<Lens> Lenses { get; } = [];

        public int FocusingPower => Lenses.Select((l, i) => l.FocalLength * (i + 1) * BoxNumber).Sum();

        public Lens? GetLens(string label) => Lenses.FirstOrDefault(l => l.Label == label);
    }

    public class Lens(string label, int focalLength)
    {
        public string Label => label;
        public int FocalLength { get; set; } = focalLength;
    }
}
