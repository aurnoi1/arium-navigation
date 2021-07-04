﻿using Arium;
using Arium.Interfaces;
using Autofac;
using FacadeExample.Pages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FacadeExample
{
    public class Map : IMapFacade
    {
        private readonly ILifetimeScope scope;
        private HashSet<DynamicNeighbor> dynamicNeighbors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="scope">The Container's scope used as DI resolver.</param>
        /// This parameter is needed for the pages resolution.</param>
        public Map(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public HashSet<INavigable> Nodes => scope.Resolve<HashSet<INavigable>>();
        public IGraph Graph => scope.Resolve<IGraph>();

        public HashSet<DynamicNeighbor> DynamicNeighbors
        {
            get
            {
                dynamicNeighbors ??= GetDynamicNeighbors();
                return dynamicNeighbors;
            }
        }

        public PageA PageA => GetPage<PageA>(Nodes);
        public PageB PageB => GetPage<PageB>(Nodes);
        public PageC PageC => GetPage<PageC>(Nodes);

        private HashSet<DynamicNeighbor> GetDynamicNeighbors()
        {
            var dynamicNeighbors = new HashSet<DynamicNeighbor>();
            foreach (var page in Nodes)
            {
                foreach (var dynamicNeighbor in page.GetDynamicNeighbors())
                {
                    dynamicNeighbors.Add(dynamicNeighbor);
                }
            }

            return dynamicNeighbors;
        }

        private T GetPage<T>(IEnumerable<INavigable> pages) where T : INavigable
        {
            Type t = typeof(T);
            var page = (T)pages.Where(p => p.GetType() == t).SingleOrDefault();
            return page;
        }
    }
}