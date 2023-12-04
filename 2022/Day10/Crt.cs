namespace Day10
{
    public class Crt
    {
        private readonly Cpu _cpu;

        public Crt(Cpu cpu)
        {
            _cpu = cpu;
        }

        public void Render()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 40; col++)
                {
                    int value = _cpu.Tick();
                    if (Math.Abs(value - col) > 1)
                        Console.Write(" ");
                    else
                        Console.Write("#");
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
