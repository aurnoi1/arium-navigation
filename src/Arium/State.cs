using Arium.Enums;
using Arium.Interfaces;

namespace Arium
{
    /// <summary>
    /// A Navigable's <see cref="State{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T> : IState<T>
    {
        /// <summary>
        /// The Navigable observed.
        /// </summary>
        public INavigable Navigable { get; }

        /// <summary>
        /// The State's name.
        /// </summary>
        public StatesNames Name { get; }

        /// <summary>
        /// The State's value.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="name">The State's name.</param>
        /// <param name="value">The State's value.</param>
        public State(INavigable navigable, StatesNames name, T value)
        {
            Navigable = navigable;
            Name = name;
            Value = value;
        }
    }
}