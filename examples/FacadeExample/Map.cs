using Arium;
using Arium.Interfaces;
using FacadeExample.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacadeExample
{
    public class Map : Mapper, IMapFacade
    {
        public Map(Lazy<HashSet<INavigable>> nodes, Lazy<IGraph> graph) : base(nodes, graph)
        {

        }

        public PageA PageA => GetNavigable<PageA>();
        public PageB PageB => GetNavigable<PageB>();
        public PageC PageC => GetNavigable<PageC>();
    }
}
