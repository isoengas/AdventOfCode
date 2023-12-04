// See https://aka.ms/new-console-template for more information

namespace Day5
{
    public static class Program
    {
        public static void Main()
        {
            var ship = new Ship(new[] {
                new[] { 'B', 'P', 'N', 'Q', 'H', 'D', 'R', 'T' },
                new[] { 'W', 'G', 'B', 'J', 'T', 'V' },
                new[] { 'N', 'R', 'H', 'D', 'S', 'V', 'M', 'Q' },
                new[] { 'P', 'Z', 'N', 'M', 'C' },
                new[] { 'D', 'Z', 'B' },
                new[] { 'V', 'C', 'W', 'Z' },
                new[] { 'G', 'Z', 'N', 'C', 'V', 'Q', 'L', 'S' },
                new[] { 'L', 'G', 'J', 'M', 'D', 'N', 'V' },
                new[] { 'T', 'P', 'M', 'F', 'Z', 'C', 'G' }
            });
            var movementsFile = File.ReadAllLines("movements.txt");
            //var result = CrateMover9000(ship, movementsFile.Select(Movement.Parse));
            var result = CrateMover9001(ship, movementsFile.Select(Movement.Parse));

            Console.WriteLine("Result 9001: ");
            Console.WriteLine(result);
        }

        private static string CrateMover9000(Ship ship, IEnumerable<Movement> movements)
        {
            foreach (var movement in movements)
            {
                ship.Move(movement.NumCrates, movement.FromStack, movement.ToStack);
            }
            return new string(ship.GetTop().ToArray());
        }

        private static string CrateMover9001(Ship ship, IEnumerable<Movement> movements)
        {
            foreach (var movement in movements)
            {
                ship.MoveMultiple(movement.NumCrates, movement.FromStack, movement.ToStack);
            }
            return new string(ship.GetTop().ToArray());
        }
        
    }
}

