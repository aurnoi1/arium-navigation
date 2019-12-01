using System;

namespace Arium.Exceptions
{
    public class UninitializedGraphException : Exception
    {
        public UninitializedGraphException() : base($"Graph is uninitialized.")
        {
        }
    }
}