


namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 5 - Print Queue");
            var lines = File.ReadAllLines("input.txt");
            var rules = new List<Rule>();
            var updates = new List<Update>();
            bool readingUpdates = false;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    readingUpdates = true;
                else if (readingUpdates)
                    updates.Add(Update.Parse(line));
                else
                    rules.Add(Rule.Parse(line));
            }
            var result = updates.Where(u => u.Passes(rules)).Sum(u => u.MiddlePage);
            Console.WriteLine("Part 1: " + result);
            result = updates.Where(u => !u.Passes(rules)).Sum(u => u.OrderedBy(rules).MiddlePage);
            Console.WriteLine("Part 2: " + result);
        }

        record Rule(int Page1, int Page2)
        {
            internal static Rule Parse(string text)
            {
                return new Rule(int.Parse(text.Split('|')[0]), int.Parse(text.Split('|')[1]));
            }
        }
        record Update(List<int> Pages)
        {
            private readonly HashSet<int> _pages = new HashSet<int>(Pages);
            internal static Update Parse(string text)
            {
                return new Update(text.Split(',').Select(int.Parse).ToList());
            }

            internal bool Passes(List<Rule> rules)
            {
                return rules.All(Pass);
            }

            internal int MiddlePage => Pages.ElementAt(Pages.Count / 2);

            private bool Pass(Rule rule)
            {
                if (!_pages.Contains(rule.Page1) || !_pages.Contains(rule.Page2))
                    return true;
                if (Pages.IndexOf(rule.Page2) > Pages.IndexOf(rule.Page1))
                    return true;
                return false;
            }

            internal Update OrderedBy(List<Rule> rules)
            {
                var affectedRules = rules.Where(r => _pages.Contains(r.Page1) && _pages.Contains(r.Page2)).ToList();
                while (!Passes(affectedRules))
                {
                    var firstNonPassingRule = affectedRules.First(r => !Pass(r));
                    Pages[Pages.IndexOf(firstNonPassingRule.Page1)] = firstNonPassingRule.Page2;
                    Pages[Pages.IndexOf(firstNonPassingRule.Page2)] = firstNonPassingRule.Page1;
                }
                return this;
            }
        }
    }
}
