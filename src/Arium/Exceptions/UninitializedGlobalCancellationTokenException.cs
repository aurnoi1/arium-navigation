using System;

namespace Arium.Exceptions
{
    public class UninitializedGlobalCancellationTokenException : Exception
    {
        public UninitializedGlobalCancellationTokenException()
            : base($"The GlobalCancellationToken is uninitialized.")
        {
        }
    }
}