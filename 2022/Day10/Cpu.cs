namespace Day10
{
    public class Cpu
    {
        private List<Instruction> _instructions;
        private int currentInstruction = 0;
        private int currentInstructionCycle = 0;
        private int currentTick = 0;

        public Cpu(List<Instruction> instructions)
        {
            X = 1;
            _instructions = instructions;
        }

        private Instruction CurrentInstruction
        {
            get
            {
                if (currentInstruction >= _instructions.Count)
                    return Instruction.Noop();

                return _instructions[currentInstruction];
            }
        }

        public int X { get; private set; }

        public int Ticks(int number)
        {
            int value = X;
            for (int i = 0; i < number; i++)
            {
                value = Tick();
            }
            return value;
        }

        public int Tick()
        {
            var valueDuringTick = X;
            currentTick++;
            currentInstructionCycle++;
            if (currentInstructionCycle == CurrentInstruction.Cycles)
            {
                Perform(CurrentInstruction);
                currentInstruction++;
                currentInstructionCycle = 0;
            }
            return valueDuringTick;
        }

        private void Perform(Instruction currentInstruction)
        {
            switch (currentInstruction)
            {
                case NoopInstruction:
                    break;
                case AddxInstruction addx:
                    X += addx.Value;
                    break;
                default:
                    break;
            }
        }
    }
}