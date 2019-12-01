﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Arium.Interfaces
{
    public interface IMap
    {
        /// <summary>
        /// The nodes of INavigables forming the Graph.
        /// </summary>
        HashSet<INavigable> Nodes { get; }

        IGraph Graph { get; }

        HashSet<DynamicNeighbor> DynamicNeighbors { get; }
    }
}
