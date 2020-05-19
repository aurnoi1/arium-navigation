using Arium.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Arium
{
    public class DynamicNeighbor
    {
        public INavigable Origin { get; }
        public HashSet<INavigable> Alternatives { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicNeighbor"/> class.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="alternatives">The alternatives.</param>
        public DynamicNeighbor(INavigable origin, HashSet<INavigable> alternatives)
        {
            Origin = origin;
            Alternatives = alternatives;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicNeighbor"/> class.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="alternatives">The alternatives.</param>
        public DynamicNeighbor(INavigable origin, params INavigable[] alternatives)
        {
            Origin = origin;
            Alternatives = alternatives.ToHashSet();
        }
    }
}