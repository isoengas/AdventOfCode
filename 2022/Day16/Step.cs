using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public abstract class Step
    {

    }

    public class OpenValve : Step
    {
        public OpenValve(string valve)
        {
            Valve = valve;
        }

        public string Valve { get; }

        public override string ToString()
        {
            return $"Open {Valve}";
        }
    }

    public class  Move : Step
    {
        public Move(Valve targetValve)
        {
            TargetValve = targetValve;
        }

        public Valve TargetValve { get; }

        public override string ToString()
        {
            return $"Move to {TargetValve.Name}";
        }
    }
}
