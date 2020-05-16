using Arium;
using Arium.Interfaces;
using Autofac;
using FacadeExample.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FacadeExample
{
    public class Map : IMap
    {
        private readonly IContainer container;

        public Map()
        {
            container = RegisterPages();
            Nodes = GetPages();
            Graph = new Graph(Nodes);
        }

        public PageA PageA => container.Resolve<PageA>();
        public PageB PageB => container.Resolve<PageB>();
        public PageC PageC => container.Resolve<PageC>();

        public HashSet<INavigable> Nodes { get; private set; }

        public IGraph Graph { get; }

        public HashSet<DynamicNeighbor> DynamicNeighbors => throw new NotImplementedException();

        private IContainer RegisterPages()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Log>().As<ILog>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BasePage>()
                .SingleInstance();

            return builder.Build();
        }

        /// <summary>
        /// Retrieve all Pages previously registered.
        /// </summary>
        /// <returns>The Pages.</returns>
        private HashSet<INavigable> GetPages()
        {
            var navigables = new HashSet<INavigable>();
            var pageTypes = container.ComponentRegistry.Registrations
                .Where(r =>
                    typeof(BasePage).IsAssignableFrom(r.Activator.LimitType)
                    && !r.Activator.LimitType.IsInterface
                    && r.Activator.LimitType.IsPublic
                    && !r.Activator.LimitType.IsAbstract
                )
                .Select(r => r.Activator.LimitType)
                .ToList();

            foreach (var pageType in pageTypes)
            {
                navigables.Add(container.Resolve(pageType) as INavigable);
            }

            return navigables;
        }
    }
}