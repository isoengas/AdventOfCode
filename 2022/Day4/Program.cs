namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sectionPairs = File.ReadAllLines("input.txt");
            int countFullyContains = 0;
            int countOverlaps = 0;
            foreach ( var sectionPair in sectionPairs )
            {
                var sections = sectionPair.Split(',');
                var section1 = SectionRange.FromText(sections[0]);
                var section2 = SectionRange.FromText(sections[1]);
                if (section1.FullyContains(section2) || section2.FullyContains(section1))
                    countFullyContains++;

                if (section1.OverlapsWith(section2))
                    countOverlaps++;
            }
            Console.WriteLine($"Count fully contains: {countFullyContains}");
            Console.WriteLine($"Count overlaps: {countOverlaps}");
        }
    }
}