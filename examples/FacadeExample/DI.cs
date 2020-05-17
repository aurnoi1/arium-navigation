using Arium;
using Arium.Interfaces;
using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using FacadeExample.Pages;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace FacadeExample
{
    public static class DI
    {
        internal static IContainer Container;

        internal static void Build(TypedParameter findControlTimeoutPara)
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<LifetimeScope>().As<ILifetimeScope>().InstancePerLifetimeScope();
            builder.RegisterType<Log>().As<ILog>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<Navigable>()
                .As<INavigable>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<Map>().As<IMap>().InstancePerLifetimeScope();

            builder.Register(c => new HashSet<INavigable>(c.Resolve<IEnumerable<INavigable>>(findControlTimeoutPara)))
                .As<HashSet<INavigable>>();

            builder.RegisterType<Graph>().As<IGraph>().InstancePerLifetimeScope();
            builder.RegisterType<Navigator>().As<INavigator>().InstancePerLifetimeScope();
            builder.RegisterType<Browser>()
                .As<IBrowser>()
                .InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}