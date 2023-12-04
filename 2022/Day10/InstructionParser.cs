namespace Day10
{
    internal static class InstructionParser
    {
        public static Instruction Parse(string input)
        {
            if (input.StartsWith("addx"))
            {
                int value = int.Parse(input.Split(' ')[1]);
                return Instruction.Addx(value);
            }
            return Instruction.Noop();
        }
    }
}