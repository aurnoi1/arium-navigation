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

        /// <summary>
        /// Get a Navigable from the node.
        /// </summary>
        /// <typeparam name="T">The returned type.</typeparam>
        /// <returns>A Navigable.</returns>
        T GetNavigable<T>() where T : INavigable;
    }
}