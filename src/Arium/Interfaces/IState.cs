using Arium.Enums;

namespace Arium.Interfaces
{
    /// <summary>
    /// Defines IState.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// The Navigable observed.
        /// </summary>
        INavigable Navigable { get; }

        /// <summary>
        /// State's name.
        /// </summary>
        StatesNames Name { get; }

        /// <summary>
        /// State's value.
        /// </summary>
        object Value { get; }
    }
}