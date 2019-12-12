﻿using Arium.Exceptions;
using Arium.Interfaces;
using System;
using System.Threading;

namespace Arium
{
    public class Browser : IBrowser
    {
        public IMap Map { get; }
        public INavigator Navigator { get; }
        public ILog Log { get; }
        public CancellationToken GlobalCancellationToken { get; private set; }

        public Browser(IMap map, ILog log, INavigator navigator, CancellationToken globalCancellationToken)
        {
            Map = map;
            Log = log;
            Navigator = navigator;
            GlobalCancellationToken = CheckCancellationToken(globalCancellationToken);
        }

        /// <summary>
        /// Executes the action passed in parameter on the last Navigable.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="action">The Action to execute.</param>m>
        /// <returns>This Browser.</returns>
        public IBrowser Do(Action action)
        {
            var last = Log.Last;
            void CreateUncancellableAction(CancellationToken ct) => action();
            Navigator.Do(last, CreateUncancellableAction, GlobalCancellationToken);
            return this;
        }

        /// <summary>
        /// Executes the action passed in parameter on the last Navigable.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="action">The Action to execute.</param>m>
        /// <returns>This Browser.</returns>
        public IBrowser Do(Action<CancellationToken> action)
        {
            var last = Log.Last;
            void createAction(CancellationToken ct) => action(ct);
            Navigator.Do(last, createAction, GlobalCancellationToken);
            return this;
        }

        /// <summary>
        /// Executes the Function passed in parameter on the last Navigable.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="function">The Function to execute.</param>
        /// <returns>This Browser.</returns>
        public IBrowser Do<T>(Func<INavigable> function) where T : INavigable
        {
            var last = Log.Last;
            INavigable CreateUncancellableFunction(CancellationToken ct) => function();
            Navigator.Do<T>(last, CreateUncancellableFunction, GlobalCancellationToken);
            return this;
        }

        /// <summary>
        /// Executes the Function passed in parameter on the last Navigable.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="function">The Function to execute.</param>
        /// <returns>This Browser.</returns>
        public IBrowser Do<T>(Func<CancellationToken, INavigable> function) where T : INavigable
        {
            var last = Log.Last;
            INavigable CreateFunction(CancellationToken ct) => function(ct);
            Navigator.Do<T>(last, CreateFunction, GlobalCancellationToken);
            return this;
        }

        /// <summary>
        /// Executes the action passed in parameter on the last Navigable.
        /// </summary>
        /// <param name="action">The Action to execute.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        public IBrowser Do(
            Action<CancellationToken> action,
            TimeSpan timeout)
        {
            using var cancellationTokenSource = new CancellationTokenSource(timeout);
            return Do(action, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Executes the action passed in parameter on the last Navigable.
        /// </summary>
        /// <param name="action">The Action to execute.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        public IBrowser Do(
            Action<CancellationToken> action,
            CancellationToken cancellationToken)
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                GlobalCancellationToken,
                cancellationToken);

            var last = Log.Last;
            linkedCts.Token.ThrowIfCancellationRequested();
            Navigator.Do(last, action, linkedCts.Token);
            return this;
        }

        /// <summary>
        /// Executes the Function passed in parameter on the last Navigable.
        /// </summary>
        /// <param name="function">The Function to execute.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        public IBrowser Do<T>(
            Func<CancellationToken, INavigable> function,
            CancellationToken cancellationToken) where T : INavigable
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                GlobalCancellationToken,
                cancellationToken);

            linkedCts.Token.ThrowIfCancellationRequested();
            var last = Log.Last;
            Navigator.Do<T>(last, function, linkedCts.Token);
            return this;
        }

        /// <summary>
        /// Executes the Function passed in parameter on the last Navigable.
        /// </summary>
        /// <param name="function">The Function to execute.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        public IBrowser Do<T>(
            Func<CancellationToken, INavigable> function,
            TimeSpan timeout) where T : INavigable
        {
            using var cancellationTokenSource = new CancellationTokenSource(timeout);
            return Do<T>(function, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Go to the destination from the last Navigable, using the shortest way.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>This Browser.</returns>
        /// <exception cref="UninitializedGraphException">Thrown when the Graph is unitialized.</exception>
        /// <exception cref="PathNotFoundException">Thrown when no path was found between the origin and the destination.</exception>
        public IBrowser Goto(INavigable destination) => Goto(destination, GlobalCancellationToken);

        /// <summary>
        /// Go to the destination from the last Navigable, using the shortest way.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        /// <exception cref="UninitializedGraphException">Thrown when the Graph is unitialized.</exception>
        /// <exception cref="PathNotFoundException">Thrown when no path was found between the origin and the destination.</exception>
        public IBrowser Goto(
        INavigable destination,
        TimeSpan timeout)
        {
            using var cancellationTokenSource = new CancellationTokenSource(timeout);
            return Goto(destination, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Go to the destination from the last Navigable, using the shortest way.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>This Browser.</returns>
        /// <exception cref="UninitializedGraphException">Thrown when the Graph is unitialized.</exception>
        /// <exception cref="PathNotFoundException">Thrown when no path was found between the origin and the destination.</exception>
        public IBrowser Goto(
                INavigable destination,
                CancellationToken cancellationToken)
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                GlobalCancellationToken,
                cancellationToken);

            linkedCts.Token.ThrowIfCancellationRequested();
            var last = Log.Last;
            Navigator.Goto(last, destination, linkedCts.Token);
            return this;
        }

        /// <summary>
        /// Go back to the previous Navigable from <see cref="ILog.Historic"/>.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <returns>The previous Navigable.</returns>
        public IBrowser Back() => Back(GlobalCancellationToken);

        /// <summary>
        /// Go back to the previous Navigable from <see cref="ILog.Historic"/>.
        /// </summary>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>The previous Navigable.</returns>
        public IBrowser Back(TimeSpan timeout)
        {
            using var cancellationTokenSource = new CancellationTokenSource(timeout);
            return Back(cancellationTokenSource.Token);
        }

        /// <summary>
        /// Go back to the previous Navigable from <see cref="ILog.Historic"/>.
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns>The previous Navigable.</returns>
        public IBrowser Back(CancellationToken cancellationToken)
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                GlobalCancellationToken,
                cancellationToken);

            linkedCts.Token.ThrowIfCancellationRequested();
            Navigator.Back(linkedCts.Token);
            return this;
        }

        /// <summary>
        /// Get the <see cref="INavigableStatus.Exist"/> status of this Navigable./>.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <returns><c>true</c> if exists, otherwise <c>false</c>.</returns>
        public bool Exists(INavigable navigable)
        {
            return navigable.PublishStatus().Exist.Value;
        }

        /// <summary>
        /// Wait until this navigable exists.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="navigable">the Navigable.</param>
        /// <returns><c>true</c> if exists before the GlobalCancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        public bool WaitForExist(INavigable navigable) => WaitForExist(navigable, GlobalCancellationToken);

        /// <summary>
        /// Wait until this navigable exists.
        /// </summary>
        /// <param name="navigable">the Navigable.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns><c>true</c> if exists before any CancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        public bool WaitForExist(INavigable navigable, CancellationToken cancellationToken)
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                GlobalCancellationToken,
                cancellationToken);

            Navigator.WaitForExist(navigable, linkedCts.Token);
            return !linkedCts.Token.IsCancellationRequested;
        }

        /// <summary>
        /// Wait until this navigable exists.
        /// </summary>
        /// <param name="navigable">the Navigable.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns><c>true</c> if exists before any CancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        public bool WaitForExist(INavigable navigable, TimeSpan timeout)
        {
            using var cancellationTokenSource = new CancellationTokenSource(timeout);
            return WaitForExist(navigable, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Wait until this navigable ready.
        /// GlobalCancellationToken will be used to interrupt the task as soon as possible.
        /// </summary>
        /// <param name="navigable">the Navigable.</param>
        /// <returns><c>true</c> if ready before the GlobalCancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        public bool WaitForReady(INavigable navigable) => WaitForReady(navigable, GlobalCancellationToken);

        /// <summary>
        /// Wait until this navigable is ready.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="cancellationToken">A CancellationToken to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns><c>true</c> if ready before any CancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        public bool WaitForReady(INavigable navigable, CancellationToken cancellationToken)
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                GlobalCancellationToken,
                cancellationToken);

            Navigator.WaitForReady(navigable, linkedCts.Token);
            return !linkedCts.Token.IsCancellationRequested;
        }

        /// <summary>
        /// Wait until this navigable is ready.
        /// </summary>
        /// <param name="navigable">The Navigable.</param>
        /// <param name="timeout">A timeout to interrupt the task as soon as possible,
        /// in concurrence of GlobalCancellationToken.</param>
        /// <returns><c>true</c> if ready before any CancellationToken is canceled.
        /// Otherwise <c>false</c>.</returns>
        public bool WaitForReady(INavigable navigable, TimeSpan timeout)
        {
            using var cancellationTokenSource = new CancellationTokenSource(timeout);
            return WaitForReady(navigable, cancellationTokenSource.Token);
        }

        private CancellationToken CheckCancellationToken(CancellationToken globalCancellationToken)
        {
            if (globalCancellationToken == null || globalCancellationToken == CancellationToken.None)
            {
                throw new UninitializedGlobalCancellationTokenException();
            }

            return globalCancellationToken;
        }
    }
}