﻿namespace Arium.Interfaces
{
    /// <summary>
    /// Defines an observer of INavigable.
    /// </summary>
    public interface INavigableObserver
    {
        /// <summary>
        /// Update the observer with this Navigable's status.
        /// </summary>
        /// <param name="status">The NavigableStatus.</param>
        void Update(INavigableStatus status);

        /// <summary>
        /// Update the observer with this Navigable's State.
        /// </summary>
        /// <param name="state">The State.</param>
        void Update(IState state);
    }
}