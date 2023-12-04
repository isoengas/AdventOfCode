namespace Day15
{
    public class Link
    {
        public Link(Point sensor, Point beacon)
        {
            Sensor = sensor;
            Beacon = beacon;
        }

        public Link((int x, int y) sensor, (int x, int y) beacon)
            :this(new Point(sensor.x, sensor.y), new Point(beacon.x, beacon.y))
        { }

        public Point Sensor { get; }
        public Point Beacon { get; }

        public int Distance => Sensor - Beacon;

        public static Link Parse(string input)
        {
            var pattern = "Sensor at x=([-\\d]+). y=([-\\d]+): closest beacon is at x=([-\\d]+), y=([-\\d]+)";
            var match = System.Text.RegularExpressions.Regex.Match(input, pattern);
            int sensorx = int.Parse(match.Groups[1].Value);
            int sensory = int.Parse(match.Groups[2].Value);
            int beaconx = int.Parse(match.Groups[3].Value);
            int beacony = int.Parse(match.Groups[4].Value);
            return new Link((sensorx, sensory), (beaconx, beacony));
        }

        public IEnumerable<(int x1, int x2)> GetExclusionRanges(int y)
        {
            int distance = this.Distance;
            var closestPoint = new Point(Sensor.X, y);
            var distanceToClosestPoint = closestPoint - Sensor;
            if (Beacon.Y == y && Beacon.X == Sensor.X) yield break;
            if (Beacon.Y == y && Beacon.X < Sensor.X)
                yield return (Beacon.X + 1, Sensor.X + (distance - distanceToClosestPoint));
            else if (Beacon.Y == y && Beacon.X > Sensor.X)
                yield return (Sensor.X - (distance - distanceToClosestPoint), Beacon.X - 1);
            else
                yield return (Sensor.X - (distance - distanceToClosestPoint), Sensor.X + (distance - distanceToClosestPoint));
        }

        public IEnumerable<(int x1, int x2)> GetExclusionRanges(int y, int maxArea)
        {
            foreach (var exclusionRange in GetExclusionRanges(y).Where(r => r.x1 <= r.x2))
            {
                yield return (exclusionRange.x1 < 0 ? 0 : exclusionRange.x1, exclusionRange.x2 > maxArea ? maxArea : exclusionRange.x2);
            }
        }
    }
}