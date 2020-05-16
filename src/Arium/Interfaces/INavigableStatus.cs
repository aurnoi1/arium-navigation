namespace Arium.Interfaces
{
    /// <summary>
    /// Defines an INavigableStatus.
    /// </summary>
    public interface INavigableStatus
    {
        /// <summary>
        /// The Navigable observed.
        /// </summary>
        INavigable Navigable { get; }

        /// <summary>
        /// The Exist state of the Navigable.
        /// </summary>
        IState<bool> Exist { get; }

        /// <summary>
        /// The Ready state.
        /// </summary>
        IState<bool> Ready { get; }
    }
}