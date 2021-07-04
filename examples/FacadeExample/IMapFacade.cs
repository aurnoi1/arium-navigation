using Arium.Interfaces;
using FacadeExample.Pages;

namespace FacadeExample
{
    public interface IMapFacade
    {
        PageA PageA { get; }
        PageB PageB { get; }
        PageC PageC { get; }
    }
}