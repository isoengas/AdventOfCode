namespace Day5
{
    public class Movement
    {
        public static Movement Parse(string movementText)
        {
            // move 5 from 4 to 9
            var words = movementText.Split(' ');
            return new Movement(int.Parse(words[1]), int.Parse(words[3]), int.Parse(words[5]));
        }

        private Movement(int numCrates, int fromStack, int toStack)
        {
            NumCrates = numCrates;
            FromStack = fromStack;
            ToStack = toStack;
        }

        public int NumCrates { get; }
        public int FromStack { get; }
        public int ToStack { get; }
    }
}
