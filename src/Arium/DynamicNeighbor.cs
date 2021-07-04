using Arium.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Arium
{
    /// <summary>
    /// Defines a DynamicNeighbor.
    /// It uses to resolve navigation multiple alternatives of
    /// Navigables can result from one action (ex: a click on a "Back" button).
    /// </summary>
    public class DynamicNeighbor
    {
        /// <summary>
        /// The origin.
        /// </summary>
        public INavigable Origin { get; }

        /// <summary>
        /// The alternatives. 
        /// </summary>
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