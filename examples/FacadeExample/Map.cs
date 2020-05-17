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
        private ILifetimeScope scope;
        private TypedParameter findControlTimeoutPara;

        public Map(ILifetimeScope scope, TimeSpan findControlTimeout)
        {
            this.scope = scope;
            findControlTimeoutPara = new TypedParameter(typeof(TimeSpan), findControlTimeout);
        }

        public HashSet<INavigable> Nodes => scope.Resolve<HashSet<INavigable>>();

        public IGraph Graph => scope.Resolve<IGraph>();

        public HashSet<DynamicNeighbor> DynamicNeighbors => throw new NotImplementedException();

        public PageA PageA => scope.Resolve<PageA>(findControlTimeoutPara);
        public PageB PageB => scope.Resolve<PageB>(findControlTimeoutPara);
        public PageC PageC => scope.Resolve<PageC>(findControlTimeoutPara);
    }
}