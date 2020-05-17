using Arium.Enums;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Arium
{
    /// <summary>
    /// Abstract implementation of <see cref="Navigable"/>.
    /// </summary>
    public abstract class Navigable : INavigable
    {
        private readonly List<WeakReference<INavigableObserver>> observers = new List<WeakReference<INavigableObserver>>();

        /// <summary>
        /// The function returning the Exist status.
        /// </summary>
        /// <returns>The Exists status.</returns>
        protected internal abstract Func<bool> Exist();

        /// <summary>
        /// The function returning the Ready status.
        /// </summary>
        /// <returns>The Ready status.</returns>
        protected internal abstract Func<bool> Ready();


        /// <summary>
        /// Register the INavigableObserver as a WeakReference.
        /// </summary>
        /// <param name="observer">The INavigableObserver.</param>
        /// <returns>The INavigableObserver as a WeakReference.</returns>
        public WeakReference<INavigableObserver> RegisterObserver(INavigableObserver observer)
        {
            var weakObserver = new WeakReference<INavigableObserver>(observer);
            observers.Add(weakObserver);
            return weakObserver;
        }

        /// <summary>
        /// Unregister the INavigableObserver.
        /// </summary>
        /// <param name="observer">The INavigableObserver.</param>
        public void UnregisterObserver(INavigableObserver observer)
        {
            var obs = observers.Where(x =>
            {
                x.TryGetTarget(out INavigableObserver target);
                return target.Equals(observer);
            })
                .SingleOrDefault();

            if (obs != null)
            {
                observers.Remove(obs);
            }
        }

        /// <summary>
        /// Notify all observers.
        /// </summary>
        /// <param name="status">The NavigableStatus.</param>
        public void NotifyObservers(INavigableStatus status)
        {
            observers.ForEach(x =>
            {
                x.TryGetTarget(out INavigableObserver obs);
                if (obs == null)
                {
                    UnregisterObserver(obs);
                }
                else
                {
                    obs.Update(status);
                }
            });
        }

        /// <summary>
        /// Notify all observers of the current state.
        /// </summary>
        /// <typeparam name="T">The state's value type.</typeparam>
        /// <param name="state">The requested State.</param>
        public void NotifyObservers<T>(IState<T> state)
        {
            observers.ForEach(x =>
            {
                x.TryGetTarget(out INavigableObserver obs);
                if (obs == null)
                {
                    UnregisterObserver(obs);
                }
                else
                {
                    obs.Update(state);
                }
            });
        }

        /// <summary>
        /// Notify observers of the current INavigable status.
        /// </summary>
        /// <returns>The published current INavigable status.</returns>
        public virtual INavigableStatus PublishStatus()
        {
            bool exist = PublishState<bool>(StatesNames.Exist).Value;
            bool ready = PublishState<bool>(StatesNames.Exist).Value;
            NavigableStatus status = new NavigableStatus(this, exist, ready);
            NotifyObservers(status);
            return status;
        }

        /// <summary>
        /// Notify observers of a specific State's value.
        /// </summary>
        /// <typeparam name="T">The State's value type.</typeparam>
        /// <param name="stateName">The state name.</param>
        /// <returns>The State.</returns>
        public virtual IState<T> PublishState<T>(StatesNames stateName)
        {
            State<bool> state = stateName switch
            {
                StatesNames.Exist => StateFactory.Create(this, StatesNames.Exist, Exist().Invoke()),
                StatesNames.Ready => StateFactory.Create(this, StatesNames.Ready, Ready().Invoke()),
                _ => throw new ArgumentException($"Undefined {nameof(StatesNames)}: {stateName}."),
            };

            NotifyObservers(state);
            return (IState<T>)state;
        }

        /// <summary>
        /// Gets a Dictionary of actions to go to the next Navigable.
        /// </summary>
        /// <returns>A Dictionary of actions to go to the next Navigable.</returns>
        public virtual Dictionary<INavigable, Action<CancellationToken>> GetActionToNext()
        {
            return new Dictionary<INavigable, Action<CancellationToken>>();
        }

        /// <summary>
        /// Get the DynamicNeighbors.
        /// </summary>
        public virtual HashSet<DynamicNeighbor> GetDynamicNeighbors()
        {
            return new HashSet<DynamicNeighbor>();
        }
    }
}
