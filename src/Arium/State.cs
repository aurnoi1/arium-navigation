using Arium.Enums;
using Arium.Interfaces;

namespace Arium
{
    /// <summary>
    /// A Navigable's <see cref="State"/>
    /// </summary>
    public class State : IState
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
        public object Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="name">The State's name.</param>
        /// <param name="value">The State's value.</param>
        public State(INavigable navigable, StatesNames name, object value)
        {
            Navigable = navigable;
            Name = name;
            Value = value;
        }
    }
}