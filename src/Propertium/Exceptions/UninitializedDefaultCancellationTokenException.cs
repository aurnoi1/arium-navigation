using System;

namespace Propertium.Exceptions
{
    public class UninitializedDefaultCancellationTokenException : Exception
    {
        public UninitializedDefaultCancellationTokenException()
            : base($"The DefaultCancellationToken is uninitialized..")
        {
        }
    }
}