using Arium.Enums;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arium
{
    /// <summary>
    /// The StateFactory.
    /// </summary>
    public static class StateFactory
    {
        /// <summary>
        /// Creates <see cref="State"/>.
        /// All Navigable's Observers are notified by the State's creation and status.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="stateName">The StatesNames' value.</param>
        /// <param name="status">The EnableState's status.</param>
        public static State Create<T>(INavigable navigable, StatesNames stateName, T status)
        {
            State state = new State(navigable, stateName, status);
            navigable.NotifyObservers(state);
            return state;
        }
    }
}
