using Arium;
using Arium.Enums;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FacadeExample.Navigables
{
    public class PageA : NavigablePublisher
    {
        public override Dictionary<INavigable, Action<CancellationToken>> GetActionToNext()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Notify observers of a specific State's value.
        /// </summary>
        /// <typeparam name="T">The State's value type.</typeparam>
        /// <param name="stateName">The state name.</param>
        /// <returns>The State.</returns>
        public override IState<T> PublishState<T>(StatesNames stateName)
        {
            bool exist = true;
            IState<T> state = stateName switch
            {
                StatesNames.Exist => (IState<T>)StateFactory.Create(this, StatesNames.Exist, exist),
                StatesNames.Ready => (IState<T>)StateFactory.Create(this, StatesNames.Ready, exist),
                _ => throw new ArgumentException($"Undefined {nameof(StatesNames)}: {stateName}."),
            };

            NotifyObservers(state);
            return state;
        }

        /// <summary>
        /// Notify observers of the current INavigable status.
        /// </summary>
        /// <returns>The published current INavigable status.</returns>
        public override INavigableStatus PublishStatus()
        {
            var exist = true;
            var enable = true;
            var navigableStatus = new NavigableStatus(this, exist, enable);
            return navigableStatus;
        }
    }
}