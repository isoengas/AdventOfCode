namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string directions = "LRLRRLLRRLRLRRLLRRLRRLLRRLRRRLRRLRRLRRRLRRLRLRLRLLLRRRLRLLRRLRLRRRLRRLLRRLRRRLRRLRLRLRRRLRLRRRLLRLLRRLRRRLLRRLRLLLRRLRLRLLRRLRLRRRLLRLLRRRLRLLRRRLRRLRRLRRRLRRRLLRLLRRRLRRRLRRLRRRLLRRRLRLRRLLRRLRLRLRRLRRLRLLRRRLRRRLLLRLRLRRRLLRRLRRRLRLRRLLRRLRRLLRLRLRRRLRLRLRLRRRLRLLRRRLRRRLRRLLLRRRR";
            var lines = File.ReadAllLines("input.txt");
            var nodes = new Dictionary<string, Node>();
            foreach (var line in lines)
            {
                string from = line[0..3];
                string left = line[7..10];
                string right = line[12..15];
                nodes[from] = new Node(left, right);
            }
            int numSteps = GetNumSteps(directions, nodes);
            Console.WriteLine("Part 1: " + numSteps);

            long numSteps2 = GetNumStepsForGhost(directions, nodes);
            Console.WriteLine("Part 2: " + numSteps2);

        }

        public static int GetNumSteps(string directions, Dictionary<string, Node> nodes)
        {
            string current = "AAA";
            int numSteps = 0;
            int currDir = 0;
            while (current != "ZZZ")
            {
                if (currDir == directions.Length)
                    currDir = 0;
                current = directions[currDir] switch
                {
                    'L' => nodes[current].Left,
                    'R' => nodes[current].Right
                };
                currDir++;
                numSteps++;
            }
            return numSteps;
        }

        public static long GetNumStepsForGhost(string directions, Dictionary<string, Node> nodes)
        {
            string[] currentPoints = nodes.Keys.Where(k => k.EndsWith('A')).ToArray();
            HashSet<string> visited = new HashSet<string>();
            long numSteps = 0;
            int currDir = 0;
            while (!currentPoints.All(c => c.EndsWith('Z')))
            {
                if (currDir == directions.Length)
                    currDir = 0;
                for (int i = 0; i < currentPoints.Length; i++)
                {
                    currentPoints[i] = directions[currDir] switch
                    {
                        'L' => nodes[currentPoints[i]].Left,
                        'R' => nodes[currentPoints[i]].Right
                    };
                }
                currDir++;
                numSteps++;
            }
            return numSteps;
        }
    }

    public class Node(string left, string right)
    {
        public string Left => left;
        public string Right => right;
    }
}
