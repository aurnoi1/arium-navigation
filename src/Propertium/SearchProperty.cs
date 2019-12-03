using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using Propertium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Propertium
{
    public class SearchProperty<W> : ISearchProperty<W> where W : IWebElement
    {
        private const string timeoutExceptionMessage = "The timeout has been reached before the Element could be found.";

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchProperty"/> class.
        /// </summary>
        /// <param name="selector">The selector to use to search the WebElement.</param>
        /// <param name="value">The expected value of the WebElement selector.</param>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="defaultTimeout">The default timeout used to cancel the task as soon as possible.</param>
        public SearchProperty(
            string selector,
            string value,
            IFindsByFluentSelector<W> webDriver,
            TimeSpan defaultTimeout)
            : this(selector, value, webDriver, 0, defaultTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchProperty"/> class.
        /// </summary>
        /// <param name="selector">The selector to use to search the WebElement.</param>
        /// <param name="value">The expected value of the WebElement selector.</param>
        /// <param name="webDriver">The WebDriver.</param>
        /// <param name="index">The index of the expected WebElement from a list of matching WebElement.</param>
        /// <param name="defaultTimeout">The default timeout used to cancel the task as soon as possible.</param>
        public SearchProperty(
            string selector,
            string value,
            IFindsByFluentSelector<W> webDriver,
            int index,
            TimeSpan defaultTimeout)
        {
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
            Index = index;
            DefaultTimeout = defaultTimeout;
        }

        /// <summary>
        /// Selector to find the WebElement.
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// Value of the Selector.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The WebDriver used to find the WebElement.
        /// </summary>
        public IFindsByFluentSelector<W> WebDriver { get; private set; }

        /// <summary>
        /// The default timeout to interrupt a search of the WebElement.
        /// </summary>
        public TimeSpan DefaultTimeout { get; private set; }

        /// <summary>
        /// The index of the WebElement in the collection of matching WebElements.
        /// </summary>
        public int Index { get; set; }

        #region Find Methods

        /// <summary>
        /// Search immediately the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// The <see cref="DefaultTimeout"/> has no effect on this task.
        /// </summary>
        /// <returns>The matching WebElement.</returns>
        /// <exception cref="WebDriverException">Thrown when no element is found.</exception>
        /// <remarks>This is the default behavior of <see cref="IFindsByFluentSelector.FindElement(string, string)"/>.</remarks>
        public W FindNow()
        {
            return WebDriver.FindElement(Selector, Value);
        }

        /// <summary>
        /// Search the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// The <see cref="DefaultTimeout"/> will be used to cancel the task.
        /// </summary>
        /// <returns>The matching WebElement.</returns>
        /// <exception cref="OperationCanceledException">Thrown when DefaultTimeout is cancelled.</exception>
        public W Find() => Find(cancellationTokens: null);

        /// <summary>
        /// Search the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// </summary>
        /// <param name="cancellationTokens">The CancellationTokens used to stop waiting for the control to be found.
        /// They will run in concurence of the <see cref="DefaultTimeout"/>.</param>
        /// <returns>The matching WebElement.</returns>
        /// <exception cref="OperationCanceledException">Thrown when any CancellationToken is cancelled.</exception>
        public W Find(params CancellationToken[] cancellationTokens)
        {
            using var defaultCancellationTokenSource = new CancellationTokenSource(DefaultTimeout);
            var linkedTokens = GetAllTokens(defaultCancellationTokenSource.Token, cancellationTokens);
            using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(linkedTokens);
            var elmt = Get(linkedTokenSource.Token);
            linkedTokenSource.Token.ThrowIfCancellationRequested();
            return elmt;
        }

        /// <summary>
        /// Search the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to wait for the control to be found.
        /// This timeout will run in concurence of the <see cref="DefaultTimeout"/>.</param>
        /// <returns>The matching WebElement.</returns>
        /// <exception cref="TimeoutException">Thrown when any timeout is reached before WebElement is found.</exception>
        public W Find(TimeSpan timeout)
        {
            using var defaultCancellationTokenSource = new CancellationTokenSource(DefaultTimeout);
            using var cts = new CancellationTokenSource(timeout);
            var linkedTokens = GetAllTokens(defaultCancellationTokenSource.Token, cts.Token);
            using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(linkedTokens);
            var elmt = Get(linkedTokenSource.Token);
            if (linkedTokenSource.Token.IsCancellationRequested)
            {
                throw new TimeoutException(timeoutExceptionMessage);
            }

            return elmt;
        }

        #endregion Find Methods

        #region Get Methods

        /// <summary>
        /// Get immediately the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// The <see cref="DefaultTimeout"/> has no effect on this task.
        /// </summary>
        /// <returns>The matching WebElement, otherwise <c>null</c>.</returns>
        public W GetNow()
        {
            return FindElement();
        }

        /// <summary>
        /// Get the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// <see cref="DefaultTimeout"/> will be used to cancel the task.
        /// </summary>
        /// <returns>The matching WebElement, otherwise <c>null</c>.</returns>
        public W Get()
        {
            return Get(cancellationTokens: null);
        }

        /// <summary>
        /// Get the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to wait for the control to be found.
        /// This timeout will run in concurence of the <see cref="DefaultTimeout"/>.</param>
        /// <returns>The matching WebElement, otherwise <c>null</c>.</returns>
        public W Get(TimeSpan timeout)
        {
            using var defaultCancellationTokenSource = new CancellationTokenSource(DefaultTimeout);
            using var cts = new CancellationTokenSource(timeout);
            var linkedTokens = GetAllTokens(defaultCancellationTokenSource.Token, cts.Token);
            using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(linkedTokens);
            return Get(linkedTokenSource.Token);
        }

        /// <summary>
        /// Get the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// </summary>
        /// <param name="cancellationTokens">The CancellationTokens used to stop waiting for the control to be found.
        /// They will run in concurence of the <see cref="DefaultTimeout"/>.</param>
        /// <returns>The matching WebElement, otherwise <c>null</c>.</returns>
        public W Get(params CancellationToken[] cancellationTokens)
        {
            using var defaultCancellationTokensource = new CancellationTokenSource(DefaultTimeout);
            var linkedTokens = GetAllTokens(defaultCancellationTokensource.Token, cancellationTokens);
            using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(linkedTokens);
            return Get(linkedTokenSource.Token);
        }

        #endregion Get Methods

        #region Private

        private W Get(CancellationToken linkedCancellationTokens)
        {
            while (!linkedCancellationTokens.IsCancellationRequested)
            {
                var match = FindElement();
                if (match != null) return match;
            }

            return default;
        }

        /// <summary>
        /// Get an array containing the a CancellationToken from DefaultTimeout and CancellationTokens[].
        /// </summary>
        /// <param name="defaultCancellationToken">The default CancellationToken from <see cref="DefaultTimeout"/>.</param>
        /// <param name="cancellationTokens">The CancellationTokens to link.</param>
        /// <returns>The CancellationTokens.</returns>
        private CancellationToken[] GetAllTokens(CancellationToken defaultCancellationToken, params CancellationToken[] cancellationTokens)
        {
            var linkedTokens = new List<CancellationToken>
            {
                defaultCancellationToken
            };

            if (cancellationTokens != null)
            {
                linkedTokens.AddRange(cancellationTokens);
            }

            return linkedTokens.ToArray();
        }

        private W FindElement()
        {
            return WebDriver
                .FindElements(Selector, Value)
                .ElementAtOrDefault(Index);
        }

        #endregion Private
    }
}