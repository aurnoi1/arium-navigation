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
        public Map(HashSet<INavigable> nodes, IGraph graph)
        {
            Nodes = nodes;
            Graph = graph;
        }

        public HashSet<INavigable> Nodes { get; private set; }

        public IGraph Graph { get; }

        public HashSet<DynamicNeighbor> DynamicNeighbors => throw new NotImplementedException();



    }
}