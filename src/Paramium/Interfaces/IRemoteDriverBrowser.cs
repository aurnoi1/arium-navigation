using Arium.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;

namespace Paramium.Interfaces
{
    public interface IRemoteDriverBrowser<R> : INavigator, IDisposable where R : IHasSessionId, IFindsByFluentSelector<IWebElement>
    {
        /// <summary>
        /// The RemoteDriver.
        /// </summary>
        R RemoteDriver { get; }
    }
}