using System;

namespace Paramium.Exceptions
{
    public class UninitializedDefaultCancellationTokenException : Exception
    {
        public UninitializedDefaultCancellationTokenException()
            : base($"The DefaultCancellationToken is uninitialized..")
        {
        }
    }
}