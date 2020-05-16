using Arium;
using Arium.Enums;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FacadeExample.Pages
{
    public class PageC : Navigable
    {
        public readonly TimeSpan ControlTimeout;
        public readonly ILog Log;

        public PageC(ILog log, TimeSpan controlTimeout)
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