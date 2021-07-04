using Arium;
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
        private HashSet<DynamicNeighbor> dynamicNeighbors;
        private readonly Lazy<HashSet<INavigable>> _nodes;
        private readonly Lazy<IGraph> _graph;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="nodes">The INaviable nodes.</param>
        /// <param name="graph">The Graph.</param>
        /// <remarks>The parameters are Lazy to prevent recursive initialization exception.</remarks>
        public Map(Lazy<HashSet<INavigable>> nodes, Lazy<IGraph> graph)
        {
            _nodes = nodes;
            _graph = graph;
        }

        public HashSet<INavigable> Nodes => _nodes.Value;
        public IGraph Graph => _graph.Value;

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