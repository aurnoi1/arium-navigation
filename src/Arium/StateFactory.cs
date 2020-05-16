using Arium.Enums;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arium
{
    /// <summary>
    /// An EnableStatus.
    /// </summary>
    public static class StateFactory
    {
        /// <summary>
        /// Creates <see cref="State{T}"/>.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="stateName">The StatesNames' value.</param>
        /// <param name="status">The EnableState's status.</param>
        public static State<T> Create<T>(INavigable navigable, StatesNames stateName, T status)
        {
            var genericIsDisplayed = (T)Convert.ChangeType(status, typeof(T));
            State<T> state = new State<T>(navigable, stateName, genericIsDisplayed);
            navigable.NotifyObservers(state);
            return state;
        }
    }
}
