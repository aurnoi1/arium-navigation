using Arium;
using Arium.Enums;
using Paramium;
using Paramium.WindowsDriver;
using Arium.Interfaces;
using AUT.Facade.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AUT.Facade.POMs
{
    public class PomMenu<R> : PomBase<R> where R : IHasSessionId, IFindsByFluentSelector<IWebElement>

    {
        public PomMenu(Map<R> map, ILog log, CancellationToken globalCancellationToken) : base(map, log, globalCancellationToken)
        {
        }

        #region Controls

        /// <summary>
        /// WDSearchProperties to find a control NOT IMPLEMENTED only use for negative test.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnNotImplemented => new SearchProperty<IWebElement>(WindowDriverLocators.AutomationId, "NotImplemented", map.RemoteDriver);

        /// <summary>
        /// WDSearchProperties to find the tile of this page.
        /// </summary>
        public SearchProperty<IWebElement> UITitle => new SearchProperty<IWebElement>(
            WindowDriverLocators.AutomationId,
            "TitleMenu", 
            map.RemoteDriver);

        /// <summary>
        /// WDSearchProperties to find a control to open the BlueView.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnOpenBluePage => new SearchProperty<IWebElement>(WindowDriverLocators.AutomationId, "BtnOpenBlueView", map.RemoteDriver);

        /// <summary>
        /// WDSearchProperties to find a control to open the RedView.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnOpenRedPage => new SearchProperty<IWebElement>(WindowDriverLocators.AutomationId, "BtnOpenRedView", map.RemoteDriver);

        /// <summary>
        /// WDSearchProperties to find a control to open the RedView.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnOpenYellowPage => new SearchProperty<IWebElement>(WindowDriverLocators.AutomationId, "BtnOpenYellowView", map.RemoteDriver);

        /// <summary>
        /// WDSearchProperties to find a control where text can be enter.
        /// </summary>
        public SearchProperty<IWebElement> UITxtBoxImportantMessage => new SearchProperty<IWebElement>(WindowDriverLocators.AutomationId, "TxtBoxImportantMessage", map.RemoteDriver);

        #endregion Controls

        /// <summary>
        /// Notify observers of a specific State's value.
        /// </summary>
        /// <typeparam name="T">The State's value type.</typeparam>
        /// <param name="stateName">The state name.</param>
        /// <returns>The State.</returns>
        public override IState<T> PublishState<T>(StatesNames stateName)
        {
            bool isDisplayed = (UITitle.Get() != null);
            var genericIsDisplayed = (T)Convert.ChangeType(isDisplayed, typeof(T));
            State<T> state = stateName switch
            {
                StatesNames.Exist => new State<T>(this, StatesNames.Exist, genericIsDisplayed),
                StatesNames.Ready => new State<T>(this, StatesNames.Ready, genericIsDisplayed),
                _ => throw new ArgumentException($"Undefined {nameof(StatesNames)}: {stateName}."),
            };

            NotifyObservers(state);
            return state;
        }

        /// <summary>
        /// Gets a Dictionary of action to go to the next INavigable.
        /// </summary>
        /// <returns>A Dictionary of action to go to the next INavigable.</returns>
        public override Dictionary<INavigable, Action<CancellationToken>> GetActionToNext()
        {
            return new Dictionary<INavigable, Action<CancellationToken>>()
            {
                { map.PomBlue, (ct) => UIBtnOpenBluePage.Find(ct).Click() },
                { map.PomRed, (ct) => UIBtnOpenRedPage.Find(ct).Click() },
                { map.PomYellow, (ct) => UIBtnOpenYellowPage.Find(ct).Click() },
            };
        }

        /// <summary>
        /// Enter a text in the UITxtBoxImportantMessage.
        /// </summary>
        /// <param name="text">The text to enter.</param>
        public void EnterText(string text)
        {
            UITxtBoxImportantMessage.Get().SendKeys(text);
        }
    }
}