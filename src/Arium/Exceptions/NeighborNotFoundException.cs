using Arium.Interfaces;
using System;

namespace Arium.Exceptions
{
    public class NeighborNotFoundException : Exception
    {
        public NeighborNotFoundException()
        {
        }

        public NeighborNotFoundException(INavigable neighbor) : base($"Could not find the neighbor \"{neighbor.GetType().FullName}\".")
        {
        }
    }
}