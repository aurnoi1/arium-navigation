using Arium;
using Arium.Interfaces;
using Autofac;
using FacadeExample.Pages;
using System;
using System.Collections.Generic;

namespace FacadeExample
{
    public class Map : IMapFacade
    {
        private readonly ILifetimeScope scope;
        private readonly TypedParameter findControlTimeoutPara;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="scope">The Container's scope used as DI resolver.</param>
        /// <param name="findControlTimeout">The timeout to find a control on a page. 
        /// This parameter is needed for the pages resolution.</param>
        public Map(ILifetimeScope scope, TimeSpan findControlTimeout)
        {
            this.scope = scope;
            findControlTimeoutPara = new TypedParameter(typeof(TimeSpan), findControlTimeout);
        }

        public HashSet<INavigable> Nodes => scope.Resolve<HashSet<INavigable>>();
        public IGraph Graph => scope.Resolve<IGraph>();
        public HashSet<DynamicNeighbor> DynamicNeighbors => new HashSet<DynamicNeighbor>();
        public PageA PageA => scope.Resolve<PageA>(findControlTimeoutPara);
        public PageB PageB => scope.Resolve<PageB>(findControlTimeoutPara);
        public PageC PageC => scope.Resolve<PageC>(findControlTimeoutPara);
    }
}