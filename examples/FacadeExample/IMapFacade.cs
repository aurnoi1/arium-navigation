using Arium.Interfaces;
using FacadeExample.Pages;

namespace FacadeExample
{
    public interface IMapFacade : IMap
    {
        PageA PageA { get; }
        PageB PageB { get; }
        PageC PageC { get; }
    }
}