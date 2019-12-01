using Arium.Interfaces;
using System.Collections.Generic;

namespace Arium
{
    public class DynamicNeighbor
    {
        public INavigable Origin { get; }
        public HashSet<INavigable> Alternatives { get; }

        public DynamicNeighbor(INavigable origin, HashSet<INavigable> alternatives)
        {
            Origin = origin;
            Alternatives = alternatives;
        }
    }
}