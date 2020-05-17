using System.Collections.Generic;

namespace Arium.Interfaces
{
    /// <summary>
    /// Represents the implementation of ILog.
    /// </summary>
    public interface ILog : INavigableObserver
    {
        /// <summary>
        /// Last known existing INavigable.
        /// </summary>
        INavigable Last { get; }

        /// <summary>
        /// Previous accessed INavigable before <see cref="Last"/>.
        /// </summary>
        INavigable Previous { get; }

        /// <summary>
        /// The historic of previsously existing INavigables.
        /// </summary>
        List<INavigable> Historic { get; }
    }
}