using Arium.Enums;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Arium.Interfaces
{
    /// <summary>
    /// Defines an INavigable.
    /// </summary>
    public interface INavigable : INavigablePublisher
    {

        /// <summary>
        /// Gets a Dictionary of action to go to the next INavigable.
        /// </summary>
        /// <returns>A Dictionary of action to go to the next INavigable.</returns>
        Dictionary<INavigable, Action<CancellationToken>> GetActionToNext();

        /// <summary>
        /// Add DynamicPaths to Map.
        /// </summary>
        HashSet<DynamicNeighbor> GetDynamicNeighbors();
    }
}