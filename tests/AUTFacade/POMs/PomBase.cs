﻿using Arium;
using Arium.Enums;
using Arium.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AUT.Facade.POMs
{
    public abstract class PomBase<R> : INavigable where R : IHasSessionId, IFindsByFluentSelector<IWebElement>
    {
        protected private readonly Map<R> map;
        private readonly List<WeakReference<INavigableObserver>> observers = new List<WeakReference<INavigableObserver>>();
        protected private readonly ILog log;
        public readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        private PomBase()
        {
        }

        public PomBase(Map<R> map, ILog log)
        {
            this.map = map;
            this.log = log;
            RegisterObserver(log);
        }

        /// <summary>
        /// Notify observers of the current INavigable status.
        /// </summary>
        /// <returns>The published current INavigable status.</returns>
        public INavigableStatus PublishStatus()
        {
            bool isDisplayed = PublishState<bool>(StatesNames.Exist).Value;
            NavigableStatus status = new NavigableStatus(this, isDisplayed, isDisplayed);
            NotifyObservers(status);
            return status;
        }

        /// <summary>
        /// Notify observers of a specific State's value.
        /// </summary>
        /// <typeparam name="T">The State's value type.</typeparam>
        /// <param name="stateName">The state name.</param>
        /// <returns>The State.</returns>
        public abstract IState<T> PublishState<T>(StatesNames stateName);

        /// <summary>
        /// Gets a Dictionary of action to go to the next INavigable.
        /// </summary>
        /// <returns>A Dictionary of action to go to the next INavigable.</returns>
        public abstract Dictionary<INavigable, Action<CancellationToken>> GetActionToNext();

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
        /// <param name="state">The State.</param>
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

        public virtual HashSet<DynamicNeighbor> GetDynamicNeighbors()
        {
            return new HashSet<DynamicNeighbor>();
        }

    }
}