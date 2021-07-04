﻿using Arium.Interfaces;
using System;

namespace FacadeExample.Pages
{
    public class PageB : BasePage
    {
        public PageB(IMapFacade map, ILog log, TestContext testContext) : base(map, log, testContext)
        {
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
        protected override Func<bool> Exist() => () => { return true; };

        /// <summary>
        /// The function returning the Ready status.
        /// </summary>
        /// <returns>The Ready status.</returns>
        protected override Func<bool> Ready() => () => { return true; };
    }
}