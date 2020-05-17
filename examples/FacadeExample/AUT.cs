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
            var findControlTimeoutPara = new TypedParameter(typeof(TimeSpan), findControlTimeout);
            DI.Build(findControlTimeoutPara);
            var scope = DI.Container.BeginLifetimeScope();
            var scopePara = new TypedParameter(typeof(ILifetimeScope), scope);
            Log = scope.Resolve<ILog>() as Log;
            Map = scope.Resolve<IMapFacade>(scopePara, findControlTimeoutPara) as Map;            
            Browser = scope.Resolve<IBrowser>(
                new TypedParameter(typeof(CancellationToken), navigationCancellation)
                ) as Browser;

        }

        public Map Map;
        public Log Log;
        public Browser Browser;

    }
}
