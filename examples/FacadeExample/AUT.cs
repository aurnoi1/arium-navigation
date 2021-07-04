using Arium;
using Arium.Interfaces;
using Autofac;
using FacadeExample.Pages;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FacadeExample
{
    public class AUT : IDisposable, IMapFacade
    {
        private bool disposed = false;
        private readonly ILifetimeScope scope;

        public AUT(TestContext testContext)
        {
            DI.Build(testContext);
            scope = DI.Container.BeginLifetimeScope();
            Log = scope.Resolve<ILog>() as Log;
            Map = scope.Resolve<IMapFacade>() as Map;
            Browser = scope.Resolve<IBrowser>(
                new TypedParameter(typeof(CancellationToken), testContext.NavigationCancellation)
                ) as Browser;
        }

        public Map Map;
        public Log Log;
        public Browser Browser;

        public PageA PageA => Map.GetNavigable<PageA>();

        public PageB PageB => Map.GetNavigable<PageB>();

        public PageC PageC => Map.GetNavigable<PageC>();

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                scope?.Dispose();
            }

            disposed = true;
        }
    }
}