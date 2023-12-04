namespace Day11
{
    public static class MonkeyParser
    {
        public static Monkey Parse(string[] lines, int decreaseWorryLevel = 3)
        {
            var items = lines[0].Substring(18).Split(',').Select(s => s.Trim()).Select(long.Parse).ToArray();
            var operation = lines[1].Substring(19);
            var test = int.Parse(lines[2].Substring(21));
            var ifTrue = int.Parse(lines[3].Substring(29));
            var ifFalse = int.Parse(lines[4].Substring(30));

            return new Monkey(items, operation, test, ifTrue, ifFalse, decreaseWorryLevel);
        }
    }
}
