namespace Day16
{
    public class Valve
    {
        public Valve(string name, int flowRate)
        {
            Name = name;
            FlowRate = flowRate;
            TargetValves = new List<Valve>();
            IsOpen = false;
        }

        public string Name { get; }
        public int FlowRate { get; }
        public bool IsOpen { get; set; }

        public void AddTunnel(params Valve[] targetValves)
        {
            TargetValves.AddRange(targetValves);
        }

        public override bool Equals(object? obj)
        {
            return obj is Valve valve &&
                   Name == valve.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public List<Valve> TargetValves { get; }

        public IEnumerable<Step> AvailableSteps
        {
            get
            {
                if (!IsOpen && FlowRate > 0)
                {
                    yield return new OpenValve(Name);
                }
                foreach (var targetValve in TargetValves)
                {
                    yield return new Move(targetValve);
                }
            }
        }

        public override string ToString() => Name;
    }
}
