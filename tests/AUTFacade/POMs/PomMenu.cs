﻿using Arium;
using Arium.Enums;
using Arium.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using Propertium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AUT.Facade.POMs
{
    public class PomMenu<R> : PomBase<R> where R : IHasSessionId, IFindsByFluentSelector<IWebElement>

    {
        public PomMenu(Map<R> map, ILog log) : base(map, log)
        {
        }

        #region Controls

        /// <summary>
        /// WDSearchProperties to find a control NOT IMPLEMENTED only use for negative test.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnNotImplemented => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "NotImplemented",
            map.RemoteDriver,
            DefaultTimeout);

        /// <summary>
        /// WDSearchProperties to find the tile of this page.
        /// </summary>
        public SearchProperty<IWebElement> UITitle => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "TitleMenu",
            map.RemoteDriver,
            DefaultTimeout);

        /// <summary>
        /// WDSearchProperties to find a control to open the BlueView.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnOpenBluePage => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "BtnOpenBlueView",
            map.RemoteDriver,
            DefaultTimeout);

        /// <summary>
        /// WDSearchProperties to find a control to open the RedView.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnOpenRedPage => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "BtnOpenRedView",
            map.RemoteDriver,
            DefaultTimeout);

        /// <summary>
        /// WDSearchProperties to find a control to open the RedView.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnOpenYellowPage => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "BtnOpenYellowView",
            map.RemoteDriver,
            DefaultTimeout);

        /// <summary>
        /// WDSearchProperties to find a control where text can be enter.
        /// </summary>
        public SearchProperty<IWebElement> UITxtBoxImportantMessage => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "TxtBoxImportantMessage",
            map.RemoteDriver,
            DefaultTimeout);

        #endregion Controls

        /// <summary>
        /// Notify observers of a specific State's value.
        /// </summary>
        /// <typeparam name="T">The State's value type.</typeparam>
        /// <param name="stateName">The state name.</param>
        /// <returns>The State.</returns>
        public override IState<T> PublishState<T>(StatesNames stateName)
        {
            bool isDisplayed = UITitle.Exist();
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