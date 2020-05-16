using Arium;
using Arium.Enums;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FacadeExample.Navigables
{
    public class PageB : NavigablePublisher, INavigable
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
            return stateName switch
            {
                StatesNames.Exist => (IState<T>)StateFactory.Create(this, StatesNames.Exist, true),
                StatesNames.Ready => (IState<T>)StateFactory.Create(this, StatesNames.Ready, true),
                _ => throw new ArgumentException("Unknown StatesNames"),
            };
        }

        /// <summary>
        /// Notify observers of the current INavigable status.
        /// </summary>
        /// <returns>The published current INavigable status.</returns>
        public override INavigableStatus PublishStatus()
        {
            throw new NotImplementedException();
        }
    }
}