using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arium
{
    /// <summary>
    /// Abstract implementation of <see cref="IMap"/>.
    /// </summary>
    abstract public class Mapper : IMap
    {
        private HashSet<DynamicNeighbor> dynamicNeighbors;
        private readonly Lazy<HashSet<INavigable>> _nodes;
        private readonly Lazy<IGraph> _graph;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mapper"/> class.
        /// </summary>
        /// <param name="nodes">The INaviable nodes.</param>
        /// <param name="graph">The Graph.</param>
        /// <remarks>The parameters are Lazy to prevent recursive initialization exception.</remarks>
        public Mapper(Lazy<HashSet<INavigable>> nodes, Lazy<IGraph> graph)
        {
            _nodes = nodes;
            _graph = graph;
        }

        /// <summary>
        /// The INaviable nodes.
        /// </summary>
        public HashSet<INavigable> Nodes => _nodes.Value;

        /// <summary>
        /// The Graph.
        /// </summary>
        public IGraph Graph => _graph.Value;

        /// <summary>
        /// The DynamicNeighbors.
        /// </summary>
        public virtual HashSet<DynamicNeighbor> DynamicNeighbors
        {
            get
            {
                dynamicNeighbors ??= GetDynamicNeighbors();
                return dynamicNeighbors;
            }
        }

        /// <summary>
        /// Get a Navigable from the node.
        /// </summary>
        /// <typeparam name="T">The returned type.</typeparam>
        /// <returns>A Navigable.</returns>
        public virtual T GetNavigable<T>() where T : INavigable
        {
            Type t = typeof(T);
            var navigable = (T)Nodes.Where(n => n.GetType() == t).SingleOrDefault();
            return navigable;
        }

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
    }
}