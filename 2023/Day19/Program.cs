using System.Security.Cryptography.X509Certificates;

namespace Day19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 19: Aplenty");
            var inputWf = File.ReadAllLines("input_wf.txt");
            var inputParts = File.ReadAllLines("input_parts.txt");
            int result = ProcessParts(inputWf, inputParts);
            Console.WriteLine("Part 1: " + result);
        }

        public static int ProcessParts(string[] inputWf, string[] inputParts)
        {
            Dictionary<string, Workflow> workflows = inputWf.Select(w => Workflow.Parse(w)).ToDictionary(w => w.Name);
            List<Part> parts = inputParts.Select(p => Part.Parse(p)).ToList();
            foreach (var part in parts)
            {
                Workflow next = workflows["in"];
                while (true)
                {
                    var result = next.Run(part);
                    if (result == "A")
                    {
                        part.IsAccepted = true;
                        break;
                    }
                    else if (result == "R")
                    {
                        break;
                    }
                    else
                    {
                        next = workflows[result];
                    }
                }
            }
            return parts.Where(p => p.IsAccepted).Sum(p => p.X + p.M + p.A + p.S);
        }
    }

    public class Workflow
    {
        public Workflow(string name, Rule[] rules)
        {
            Name = name;
            _rules = rules;
        }
        public string Name { get; }
        private Rule[] _rules;

        public static Workflow Parse(string instructions)
        {
            return new Workflow(instructions.Split('{')[0], instructions.Split('{')[1].Split('}')[0].Split(',').Select(r => Rule.Parse(r)).ToArray());
        }

        public string Run(Part part)
        {
            return _rules.Select(r => r.Run(part)).First(dest => dest != null);
        }
    }

    public class Rule
    {
        private Rule(Condition condition, string destination)
        {
            Condition = condition;
            Destination = destination;
        }
        public static Rule Parse(string rule)
        {
            if (rule.Contains(':'))
            {
                return new Rule(Condition.Parse(rule.Split(':')[0]), rule.Split(':')[1]);
            }
            else
            {
                return new Rule(Condition.True(), rule);
            }
        }

        public string? Run(Part part)
        {
            if (Condition.Check(part))
                return Destination;
            return null;
        }
        private Condition Condition { get; set; }
        private string Destination { get; set; }
    }

    public class Condition
    {
        private static Condition _true = new Condition { Check = _ => true };
        public static Condition Parse(string condition)
        {
            var c = new Condition();
            if (condition.Contains("<"))
            {
                int value = int.Parse(condition.Split('<')[1]);
                if (condition[0] == 'x')
                {
                    c.Check = p => p.X < value;
                }
                else if (condition[0] == 'm')
                {
                    c.Check = p => p.M < value;
                }
                else if (condition[0] == 'a')
                {
                    c.Check = p => p.A < value;
                }
                else
                {
                    c.Check = p => p.S < value;
                }
            }
            else if (condition.Contains(">"))
            {
                int value = int.Parse(condition.Split('>')[1]);
                if (condition[0] == 'x')
                {
                    c.Check = p => p.X > value;
                }
                else if (condition[0] == 'm')
                {
                    c.Check = p => p.M > value;
                }
                else if (condition[0] == 'a')
                {
                    c.Check = p => p.A > value;
                }
                else
                {
                    c.Check = p => p.S > value;
                }
            }
            else
            {
                throw new InvalidOperationException("Unexpected condition: " + condition);
            }
            return c;
        }

        public static Condition True() => _true;

        public Predicate<Part> Check { get; private set; } = (_) => true;
    }

    public class Part
    {
        public Part(int x, int m, int a, int s)
        {
            X = x;
            M = m;
            A = a;
            S = s;
        }
        public static Part Parse(string part)
        {
            var p = part.TrimStart('{').TrimEnd('}');
            var ps = p.Split(',');
            int x = int.Parse(ps[0].Split('=')[1]);
            int m = int.Parse(ps[1].Split('=')[1]);
            int a = int.Parse(ps[2].Split('=')[1]);
            int s = int.Parse(ps[3].Split('=')[1]);
            return new Part(x, m, a, s);
        }
        public int X { get; }
        public int M { get; }
        public int A { get; }
        public int S { get; }

        public bool IsAccepted { get; set; }
    }
}
