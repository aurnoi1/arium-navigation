using Arium.Interfaces;
using System;

namespace Arium.Exceptions
{
    public class UnregistredNeighborException : Exception
    {
        public UnregistredNeighborException()
        {
        }

        public UnregistredNeighborException(INavigable neighbor, Type declaringClass)
            : base($"\"{neighbor.GetType().FullName}\" is not registred in \"{declaringClass.GetType().FullName}\".")
        {
        }
    }
}