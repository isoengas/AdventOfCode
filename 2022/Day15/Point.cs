namespace Day15
{
    public record Point(int X, int Y)
    {
        public static int operator -(Point pointa, Point pointb) => Math.Abs(pointa.X - pointb.X) + Math.Abs(pointa.Y - pointb.Y);

        public bool CanExist(IEnumerable<Link> links)
        {
            return !links.Any(link => link.Sensor - this <= link.Distance && link.Beacon != this);
        }
    }
}