using Arium.Enums;
using Arium.Interfaces;

namespace Arium
{
    /// <summary>
    /// A NavigableStatus
    /// </summary>
    public class NavigableStatus : INavigableStatus
    {
        /// <summary>
        /// The Exist status.
        /// </summary>
        public IState<bool> Exist { get; private set; }

        /// <summary>
        /// The Ready status.
        /// </summary>
        public IState<bool> Ready { get; private set; }

        /// <summary>
        /// The Navigable observed.
        /// </summary>
        public INavigable Navigable { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigableStatus"/> class.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="exist">The Navigable's Exist IState.</param>
        /// <param name="ready">The Navigable's Ready IState.</param>
        public NavigableStatus(INavigable navigable, bool exist, bool ready)
        {
            Navigable = navigable;
            Exist = new State<bool>(navigable, StatesNames.Exist, exist);
            Ready = new State<bool>(navigable, StatesNames.Ready, ready);
        }
    }
}