using Arium;
using Arium.Enums;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FacadeExample.Pages
{
    public class PageC : BasePage
    {
        public PageC(IMapFacade map, ILog log, TestContext testContext) : base(map, log, testContext)
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

        /// <summary>
        /// Gets a Dictionary of action to go to the next INavigable.
        /// </summary>
        /// <returns>A Dictionary of action to go to the next INavigable.</returns>
        public override Dictionary<INavigable, Action<CancellationToken>> GetActionToNext()
        {
            return new Dictionary<INavigable, Action<CancellationToken>>()
            {
                { Map.PageA, (ct) => OpenPageA(ct) },
            };
        }

        public PageA OpenPageA(CancellationToken cancellationToken)
        {
            // Insert implementation to open PageB here.
            // This method should returns the page.

            // Use the cancellationToken to stop operation as
            // soon as possible.

            return Map.PageA;
        }
    }
}