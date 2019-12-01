using AUT.Facade.POMs;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;

namespace AUT.Facade.Interfaces
{
    public interface INavigables<R> where R : IHasSessionId, IFindsByFluentSelector<IWebElement>
    {
        PomBlue<R> PomBlue { get; }
        PomMenu<R> PomMenu { get; }
        PomRed<R> PomRed { get; }
        PomYellow<R> PomYellow { get; }
    }
}