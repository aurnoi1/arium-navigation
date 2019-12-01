using System;

namespace Arium.Exceptions
{
    public class UnregistredNodeException : Exception
    {
        public UnregistredNodeException()
        {
        }

        public UnregistredNodeException(Type nodeType) : base($"Unregistred Node: \"{nodeType.FullName}\".")
        {
        }
    }
}