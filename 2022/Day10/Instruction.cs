namespace Day10
{
    public abstract class Instruction
    {
        public static Instruction Noop()
        {
            return new NoopInstruction();
        }

        public static Instruction Addx(int value)
        {
            return new AddxInstruction(value);
        }

        public abstract int Cycles { get; }
    }

    internal class AddxInstruction : Instruction
    {
        public AddxInstruction(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override int Cycles => 2;
    }

    public class NoopInstruction : Instruction
    {
        public override int Cycles => 1;
    }
}