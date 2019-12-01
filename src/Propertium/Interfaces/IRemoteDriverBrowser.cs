using Arium.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;

namespace Propertium.Interfaces
{
    public interface IRemoteDriverBrowser<R> : INavigator, IDisposable where R : IHasSessionId, IFindsByFluentSelector<IWebElement>
    {
        /// <summary>
        /// The RemoteDriver.
        /// </summary>
        R RemoteDriver { get; }
    }
}