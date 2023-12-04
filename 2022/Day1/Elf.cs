namespace Day1
{
    internal class Elf
    {
        public int TotalCallories { get; private set; }

        internal void AddCallories(int callories)
        {
            TotalCallories += callories;
        }
    }
}