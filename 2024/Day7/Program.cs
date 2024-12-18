namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 7 - Bridge Repair");
            var lines = ReadInput();
            var equations = lines.Select(l => Equation.Parse(l)).ToList();
            var result = equations.Where(e => e.IsSolvable(false)).Sum(e => e.Result);
            Console.WriteLine("Part 1: " + result);
            var result2 = equations.Where(e => e.IsSolvable(true)).Sum(e => e.Result);
            Console.WriteLine("Part 2: " + result2);
        }

        static string[] ReadInput()
        {
            return File.ReadAllLines("input.txt");
        }
        static string[] ReadTestInput()
        {
            return [
                "190: 10 19",
                "3267: 81 40 27",
                "83: 17 5",
                "156: 15 6",
                "7290: 6 8 6 15",
                "161011: 16 10 13",
                "192: 17 8 14",
                "21037: 9 7 18 13",
                "292: 11 6 16 20"
                ];
        }
    }

    internal class Equation
    {
        internal long Result { get; private set; }
        private readonly List<long> _operands;

        public Equation(long result, IEnumerable<long> operands)
        {
            Result = result;
            _operands = operands.ToList();
        }
        internal static Equation Parse(string text)
        {
            long result = long.Parse(text.Split(':')[0]);
            List<long> operands = text.Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            return new Equation(result, operands);
        }
        internal bool IsSolvable(bool withConcatenation)
        {
            if (_operands[0] > Result)
                return false;

            if (_operands.Count == 1)
                return _operands[0] == Result;

            var rest = _operands[2..];
            var withSum = new Equation(Result, new List<long>() { _operands[0] + _operands[1] }.Concat(rest));
            var withMul = new Equation(Result, new List<long>() { _operands[0] * _operands[1] }.Concat(rest));
            if (withConcatenation)
            {
                var withConc = new Equation(Result, new List<long>() { long.Parse($"{_operands[0]}{_operands[1]}") }.Concat(rest));
                return withSum.IsSolvable(true) || withMul.IsSolvable(true) || withConc.IsSolvable(true);
            }
            else
            {
                return withSum.IsSolvable(false) || withMul.IsSolvable(false);
            }

        }
    }
}
