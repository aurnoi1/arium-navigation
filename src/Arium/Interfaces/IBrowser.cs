using Arium.Exceptions;
using System;
using System.Threading;

namespace Arium.Interfaces
{
    /// <summary>
    /// Defines a Browser.
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        /// The CancellationToken set for browsing session.
        /// </summary>
        CancellationToken GlobalCancellationToken { get; }

        /// <summary>
        /// The Log used to register navigation.
        /// </summary>
        ILog Log { get; }

        /// <summary>
        /// The Map used for navigation.
        /// </summary>
        IMap Map { get; }

        /// <summary>
        /// The Navigator.
        /// </summary>
        INavigator Navigator { get; }

        /// <summary>
        /// Go back to the previous Navigable from <see cref="ILog.Historic"/>.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <returns>The previous Navigable.</returns>
        IBrowser Back();

        /// <summary>
        /// Go back to the previous Navigable from <see cref="ILog.Historic"/>.
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>The previous Navigable.</returns>
        IBrowser Back(CancellationToken cancellationToken);

        /// <summary>
        /// Go back to the previous Navigable from <see cref="ILog.Historic"/>.
        /// </summary>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>The previous Navigable.</returns>
        IBrowser Back(TimeSpan timeout);

        /// <summary>
        /// Executes the action passed in parameter on the last Navigable.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="action">The Action to execute.</param>m>
        /// <returns>This Browser.</returns>
        IBrowser Do(Action action);

        /// <summary>
        /// Executes the action passed in parameter on the last Navigable.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="action">The Action to execute.</param>m>
        /// <returns>This Browser.</returns>
        IBrowser Do(Action<CancellationToken> action);

        /// <summary>
        /// Executes the action passed in parameter on the last Navigable.
        /// </summary>
        /// <param name="action">The Action to execute.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        IBrowser Do(Action<CancellationToken> action, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the action passed in parameter on the last Navigable.
        /// </summary>
        /// <param name="action">The Action to execute.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        IBrowser Do(Action<CancellationToken> action, TimeSpan timeout);

        /// <summary>
        /// Executes the Function passed in parameter on the last Navigable.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="function">The Function to execute.</param>
        /// <returns>This Browser.</returns>
        public IBrowser Do<T>(Func<CancellationToken, INavigable> function) where T : INavigable;

        /// <summary>
        /// Executes the Function passed in parameter on the last Navigable.
        /// </summary>
        /// <param name="function">The Function to execute.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        IBrowser Do<T>(Func<CancellationToken, INavigable> function, CancellationToken cancellationToken) where T : INavigable;

        /// <summary>
        /// Executes the Function passed in parameter on the last Navigable.
        /// </summary>
        /// <param name="function">The Function to execute.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        IBrowser Do<T>(Func<CancellationToken, INavigable> function, TimeSpan timeout) where T : INavigable;

        /// <summary>
        /// Executes the Function passed in parameter on the last Navigable.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="function">The Function to execute.</param>
        /// <returns>This Browser.</returns>
        IBrowser Do<T>(Func<INavigable> function) where T : INavigable;

        /// <summary>
        /// Get the <see cref="INavigableStatus.Exist"/> status of this Navigable./>.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <returns><c>true</c> if exists, otherwise <c>false</c>.</returns>
        bool Exists(INavigable navigable);

        /// <summary>
        /// Go to the destination from the last Navigable, using the shortest way.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>This Browser.</returns>
        /// <exception cref="UninitializedGraphException">Thrown when the Graph is unitialized.</exception>
        /// <exception cref="PathNotFoundException">Thrown when no path was found between the origin and the destination.</exception>
        IBrowser Goto(INavigable destination);

        /// <summary>
        /// Go to the destination from the last Navigable, using the shortest way.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        /// <exception cref="UninitializedGraphException">Thrown when the Graph is unitialized.</exception>
        /// <exception cref="PathNotFoundException">Thrown when no path was found between the origin and the destination.</exception>
        IBrowser Goto(INavigable destination, CancellationToken cancellationToken);

        /// <summary>
        /// Go to the destination from the last Navigable, using the shortest way.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        /// <exception cref="UninitializedGraphException">Thrown when the Graph is unitialized.</exception>
        /// <exception cref="PathNotFoundException">Thrown when no path was found between the origin and the destination.</exception>
        IBrowser Goto(INavigable destination, TimeSpan timeout);

        /// <summary>
        /// Wait until this navigable exists.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="navigable">the Navigable.</param>
        /// <returns><c>true</c> if exists before the GlobalCancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        bool WaitForExist(INavigable navigable);

        /// <summary>
        /// Wait until this navigable exists.
        /// </summary>
        /// <param name="navigable">the Navigable.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns><c>true</c> if exists before any CancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        bool WaitForExist(INavigable navigable, CancellationToken cancellationToken);

        /// <summary>
        /// Wait until this navigable exists.
        /// </summary>
        /// <param name="navigable">the Navigable.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns><c>true</c> if exists before any CancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        bool WaitForExist(INavigable navigable, TimeSpan timeout);

        /// <summary>
        /// Wait until this navigable is ready.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns><c>true</c> if ready before any CancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        bool WaitForReady(INavigable navigable, CancellationToken cancellationToken);

        /// <summary>
        /// Wait until this navigable is ready.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns><c>true</c> if ready before any CancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        bool WaitForReady(INavigable navigable, TimeSpan timeout);

        /// <summary>
        /// Wait until this navigable ready.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="navigable">the Navigable.</param>
        /// <returns><c>true</c> if ready before the GlobalCancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        bool WaitForReady(INavigable navigable);
    }
}