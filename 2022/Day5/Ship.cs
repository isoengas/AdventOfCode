namespace Day5
{
    public class Ship
    {
        private readonly Stack<char>[] _stacks;

        public Ship(IEnumerable<char[]> stacks)
        {
            _stacks = stacks.Select(s => new Stack<char>(s)).ToArray();
        }

        public IEnumerable<char> GetTop()
        {
            return _stacks.Select(s => s.TryPeek(out char c) ? c : ' ');
        }

        public void Move(int numCrates, int fromStack, int toStack)
        {
            for (int i = 0; i < numCrates; i++)
            {
                char crate = _stacks[fromStack - 1].Pop();
                _stacks[toStack - 1].Push(crate);
            }
        }

        internal void MoveMultiple(int numCrates, int fromStack, int toStack)
        {
            var tempStack = new Stack<char>();
            for (int i = 0; i < numCrates; i++)
            {
                char crate = _stacks[fromStack - 1].Pop();
                tempStack.Push(crate);
            }
            while (tempStack.Count > 0)
            {
                char crate = tempStack.Pop();
                _stacks[toStack -1].Push(crate);
            }
        }
    }
}
