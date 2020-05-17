using Arium.Enums;
using Arium.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Arium
{
    /// <summary>
    /// A Log.
    /// </summary>
    public class Log : ILog
    {
        /// <summary>
        /// Lock to queue historic entries.
        /// </summary>
        private readonly object historicLock = new object();

        /// <summary>
        /// 
        /// Previous accessed INavigable before <see cref="Last"/>.
        /// </summary>
        public virtual INavigable Previous
        {
            get
            {
                lock (historicLock)
                {
                    return Historic.Count > 1 ? Historic[Historic.Count - 2] : null;
                }
            }
        }

        /// <summary>
        /// Last known existing INavigable.
        /// </summary>
        public INavigable Last
        {
            get
            {
                lock (historicLock)
                {
                    return Historic.LastOrDefault();
                }
            }

            private set
            {
                lock (historicLock)
                {
                    if (value != null && value != Historic.LastOrDefault())
                    {
                        Historic.Add(value);
                    }
                }
            }
        }

        /// <summary>
        /// The historic of previsously existing INavigables.
        /// </summary>
        public List<INavigable> Historic { get; private set; } = new List<INavigable>();

        /// <summary>
        /// Update the observer with this Navigable's status.
        /// </summary>
        /// <param name="status">The NavigableStatus.</param>
        public void Update(INavigableStatus status)
        {
            SetLast(status.Exist);
        }

        /// <summary>
        /// Update the observer with this Navigable's State.
        /// </summary>
        /// <param name="state">The State.</param>
        public void Update(IState state)
        {
            if (state.Name == StatesNames.Exist)
            {
                Last = state.Navigable;
            }
        }

        /// <summary>
        /// Set the last known INavigable is exists.
        /// </summary>
        /// <param name="state">A State of the last INavigable.</param>
        /// <returns><c>true</c> if the INavigable exists, otherwise <c>false</c>.</returns>
        private void SetLast(IState state)
        {
            if (state.Name == StatesNames.Exist)
            {
                Last = state.Navigable;
            }
        }
    }
}