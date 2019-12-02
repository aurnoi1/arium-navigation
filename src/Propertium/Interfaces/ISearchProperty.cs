using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Threading;

namespace Propertium.Interfaces
{
    public interface ISearchProperty<W> where W : IWebElement
    {
        /// <summary>
        /// The default CancellationToken to interrupt a search of the WebElement.
        /// </summary>
        CancellationToken DefaultCancellationToken { get; }

        /// <summary>
        /// The index of the WebElement in the collection of matching WebElements.
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Locator to find the WebElement.
        /// </summary>
        string Locator { get; set; }

        /// <summary>
        /// Value of the Locator.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// The WebDriver used to find the WebElement.
        /// </summary>
        IFindsByFluentSelector<W> WebDriver { get; }

        /// <summary>
        /// Search immediately the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// The <see cref="DefaultCancellationToken"/> has no effect on this task.
        /// </summary>
        /// <returns>The matching WebElement.</returns>
        /// <exception cref="WebDriverException">Thrown when no element is found.</exception>
        /// <remarks>This is the default behavior of <see cref="IFindsByFluentSelector.FindElement(string, string)"/>.</remarks>
        W FindNow();

        /// <summary>
        /// Search the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// The <see cref="DefaultCancellationToken"/> will be used to cancel the task.
        /// </summary>
        /// <returns>The matching WebElement.</returns>
        /// <exception cref="OperationCanceledException">Thrown when DefaultCancellationToken is cancelled.</exception>
        W Find();

        /// <summary>
        /// Search the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// </summary>
        /// <param name="cancellationTokens">The CancellationTokens used to stop waiting for the control to be found.
        /// They will run in concurence of the <see cref="DefaultCancellationToken"/>.</param>
        /// <returns>The matching WebElement.</returns>
        /// <exception cref="OperationCanceledException">Thrown when any CancellationToken is cancelled.</exception>
        W Find(params CancellationToken[] cancellationTokens);

        /// <summary>
        /// Search the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to wait for the control to be found.
        /// This timeout will run in concurence of the <see cref="DefaultCancellationToken"/>.</param>
        /// <returns>The matching WebElement.</returns>
        /// <exception cref="TimeoutException">Thrown when any timeout is reached before WebElement is found.</exception>
        W Find(TimeSpan timeout);

        /// <summary>
        /// Get immediately the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// The <see cref="DefaultCancellationToken"/> has no effect on this task.
        /// </summary>
        /// <returns>The matching WebElement, otherwise <c>null</c>.</returns>
        W GetNow();

        /// <summary>
        /// Get the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// <see cref="DefaultCancellationToken"/> will be used to cancel the task.
        /// </summary>
        /// <returns>The matching WebElement, otherwise <c>null</c>.</returns>
        W Get();

        /// <summary>
        /// Get the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// </summary>
        /// <param name="cancellationTokens">The CancellationTokens used to stop waiting for the control to be found.
        /// They will run in concurence of the <see cref="DefaultCancellationToken"/>.</param>
        /// <returns>The matching WebElement, otherwise <c>null</c>.</returns>
        W Get(params CancellationToken[] cancellationTokens);

        /// <summary>
        /// Get the WebElement of type <typeparamref name="W"/> matching the SearchProperty.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to wait for the control to be found.
        /// This timeout will run in concurence of the <see cref="DefaultCancellationToken"/>.</param>
        /// <returns>The matching WebElement, otherwise <c>null</c>.</returns>
        W Get(TimeSpan timeout);
    }
}