using System.Collections.Generic;

namespace Arium.Interfaces
{
    public interface IMap
    {
        /// <summary>
        /// The nodes of INavigables forming the Graph.
        /// </summary>
        HashSet<INavigable> Nodes { get; }

        /// <summary>
        /// The Graph of Navigables.
        /// </summary>
        IGraph Graph { get; }

        HashSet<DynamicNeighbor> DynamicNeighbors { get; }
    }
}