using Arium.Enums;

namespace Arium.Interfaces
{
    public interface IState<T>
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
        T Value { get; }
    }
}