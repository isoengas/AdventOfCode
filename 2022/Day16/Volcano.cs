using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Payloads;

namespace Day16
{
    internal class Volcano : Dictionary<string, Valve>
    {
        internal void AddTunnel(string fromValve, string toValve)
        {
            this[fromValve].AddTunnel(this[toValve]);
        }

        internal int ComputePath(Valve fromValve, int minutes, Step[] path)
        {
            int currentStep = 0;
            Valve currentValve = fromValve;
            int pressureReleased = 0;
            for (int i = 1; i <= minutes; i++)
            {
                pressureReleased += this.Values.Where(v => v.IsOpen).Sum(v => v.FlowRate);
                if (currentStep >= path.Length)
                    continue;

                switch (path[currentStep])
                {
                    case Move move:
                        currentValve = move.TargetValve;
                        currentStep++;
                        break;
                    case OpenValve open:
                        currentValve.IsOpen = true;
                        currentStep++;
                        break;
                }
            }
            return pressureReleased;
        }

        internal Dictionary<Valve, Dictionary<Valve, Valve[]>> GetMinPaths()
        {
            var result = new Dictionary<Valve, Dictionary<Valve, Valve[]>>();
            foreach (var v in Values)
            {
                var r = Dijkstra.Run(v, Values);
                result.Add(v, new Dictionary<Valve, Valve[]>());
                foreach (var t in r.dist.Where(d => d.Value > 0))
                {
                    result[v][t.Key] = GetIntermediaryNodes(v, t.Key, r.prev);
                }
            }
            return result; 
        }

        internal (IEnumerable<Step>, Valve) GetNextSteps(Valve from, int minutesLeft)
        {
            var r = Dijkstra.Run(from, Values);
            var rewardPerValve = r.dist
                                    .Select(d => new { TargetValve = d.Key, Reward = (minutesLeft - d.Value) * (d.Key.IsOpen ? 0 : d.Key.FlowRate) })
                                    .OrderByDescending(r => r.Reward);
            var valveWithHigherReward = rewardPerValve.First().TargetValve;
            var intermediaryNodes = GetIntermediaryNodes(from, valveWithHigherReward, r.prev);
            var steps = intermediaryNodes.Select(n => new Move(n)).Cast<Step>()
                            .Append(new Move(valveWithHigherReward))
                            .Append(new OpenValve(valveWithHigherReward.Name));
            return (steps, valveWithHigherReward);
        }

        private Valve[] GetIntermediaryNodes(Valve from, Valve to, Dictionary<Valve, Valve> prev)
        {
            var path = new Stack<Valve>();
            var u = to;
            if (prev[to] != null || u == from)
            {
                while (u != null)
                {
                    path.Push(u);
                    u = prev[u];
                }
            }
            return path.Skip(1).SkipLast(1).ToArray();
        }

        internal IEnumerable<Step> Compute(Valve from, int minutesLeft)
        {
            var result = new List<Step>();
            int left = minutesLeft;
            var currentValve = from;
            while (left > 0)
            {
                var (nextSteps, nextValve) = GetNextSteps(currentValve, left);
                result.AddRange(nextSteps);
                currentValve = nextValve;
                currentValve.IsOpen = true;
                left -= nextSteps.Count();
            }
            return result.Take(minutesLeft);
        }

        internal void Reset()
        {
            foreach (var valve in Values)
                valve.IsOpen = false;
        }
    }
}