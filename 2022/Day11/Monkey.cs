namespace Day11
{
    public class Monkey
    {
        private readonly List<long> items;
        private readonly string _operation;
        private readonly int _test;
        private readonly int _ifTrue;
        private readonly int _ifFalse;
        private readonly int _decreaseWorryLevel;

        public Monkey(long[] startingItems, string operation, int test, int ifTrue, int ifFalse, int decreaseWorryLevel = 3)
        {
            items = startingItems.ToList();
            _operation = operation;
            _test = test;
            _ifTrue = ifTrue;
            _ifFalse = ifFalse;
            _decreaseWorryLevel = decreaseWorryLevel;
            NumInspectedItems = 0;
        }

        public long NumInspectedItems { get; private set; }

        public IEnumerable<long> Items => items;
        public string Operation => _operation;
        public int Test => _test;
        public int IfTrue => _ifTrue;
        public int IfFalse => _ifFalse;


        public IEnumerable<Throw> Turn()
        {
            foreach (int item in items)
            {
                NumInspectedItems++;
                long worryLevel = Perform(_operation, item);
                worryLevel /= _decreaseWorryLevel;
                int targetMonkey = (worryLevel % _test == 0) ? _ifTrue : _ifFalse;
                yield return new Throw(worryLevel, targetMonkey);
            }
            items.Clear();
        }

        internal void ThrowItem(long item)
        {
            items.Add(item);
        }

        private long Perform(string operation, long item)
        {
            var parts = operation.Split(' ');
            long op1 = parts[0] == "old" ? item : long.Parse(parts[0]);
            long op2 = parts[2] == "old" ? item : long.Parse(parts[2]);
            switch (parts[1])
            {
                case "+":
                    return op1 + op2;
                case "*":
                    return op1 * op2;
                default:
                    throw new NotImplementedException();
            }    
        }
    }

    public record Throw(long item, int monkey);
}
