namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var packetDetector = new MarkerDetector(4);
            int firstPacketPosition = packetDetector.GetFirstMarkerPosition(input);
            Console.WriteLine($"First packet marker found after character {firstPacketPosition}");

            var messageDetector = new MarkerDetector(14);
            int firstMessagePosition = messageDetector.GetFirstMarkerPosition(input);
            Console.WriteLine($"First message marker found after character {firstMessagePosition}");
        }
    }
}