using Arium;
using Arium.Interfaces;
using System;

namespace FacadeExample.Pages
{
    public class PageA : Navigable
    {
        public readonly TimeSpan ControlTimeout;
        public readonly ILog Log;

        public PageA(ILog log, TimeSpan controlTimeout)
        {
            Log = log;
            ControlTimeout = controlTimeout;
            RegisterObserver(log);
        }

        ///// <summary>
        ///// Gets a Dictionary of actions to go to the next Navigable.
        ///// </summary>
        ///// <returns>A Dictionary of actions to go to the next Navigable.</returns>
        //public override Dictionary<INavigable, Action<CancellationToken>> GetActionToNext()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// The function returning the Exist status.
        /// </summary>
        /// <returns>The Exists status.</returns>
        public override Func<bool> Exist() => () => { return true; };

        /// <summary>
        /// The function returning the Ready status.
        /// </summary>
        /// <returns>The Ready status.</returns>
        public override Func<bool> Ready() => () => { return true; };
    }
}