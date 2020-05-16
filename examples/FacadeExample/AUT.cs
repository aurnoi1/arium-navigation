using Arium;
using Arium.Interfaces;
using Autofac;
using Autofac.Core;
using FacadeExample.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FacadeExample
{
    public class AUT
    {

        public AUT(TimeSpan findControlTimeout, CancellationToken navigationCancellation)
        {
            DI.Build();
            var findControlTimeoutPara = new TypedParameter(typeof(TimeSpan), findControlTimeout);
            using var scope = DI.Container.BeginLifetimeScope();
            Log = scope.Resolve<ILog>();
            PageA = scope.Resolve<PageA>(findControlTimeoutPara);
            PageB = scope.Resolve<PageB>(findControlTimeoutPara);
            PageC = scope.Resolve<PageC>(findControlTimeoutPara);
            Map = scope.Resolve<IMap>();
            Browser = scope.Resolve<IBrowser>(
                new TypedParameter(typeof(CancellationToken), navigationCancellation)
                );

        }

        public IMap Map;
        public ILog Log;
        public IBrowser Browser;
        public PageA PageA;
        public PageB PageB;
        public PageC PageC;

    }
}
