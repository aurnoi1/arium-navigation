using Arium;
using Arium.Interfaces;
using System;
using System.Collections.Generic;

namespace FacadeExample.Pages
{
    public class PageA : BasePage
    {
        public PageA(IMapFacade map, ILog log, TestContext testContext) : base(map, log, testContext)
        {
        }

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