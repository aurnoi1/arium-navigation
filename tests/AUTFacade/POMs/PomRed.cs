using Arium;
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
    public class PomRed<R> : PomBase<R> where R : IHasSessionId, IFindsByFluentSelector<IWebElement>
    {
        public PomRed(Map<R> map, ILog log, CancellationToken globalCancellationToken) : base(map, log, globalCancellationToken)
        {
        }

        #region Controls

        /// <summary>
        /// WDSearchProperties to find the tile of this page.
        /// </summary>
        public SearchProperty<IWebElement> UITitle => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "TitleRed",
            map.RemoteDriver,
            globalCancellationToken);

        /// <summary>
        /// WDSearchProperties to find a control to open the previous page.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnBack => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "BtnBack",
            map.RemoteDriver,
            globalCancellationToken);

        /// <summary>
        /// WDSearchProperties to find a control to open the yellow page.
        /// </summary>
        public SearchProperty<IWebElement> UIBtnOpenYellowPage => new SearchProperty<IWebElement>(
            MobileSelector.Accessibility,
            "BtnOpenYellowView",
            map.RemoteDriver,
            globalCancellationToken);

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
                { map.PomMenu, (ct) => UIBtnBack.Find(ct).Click() },
                { map.PomYellow, (ct) => UIBtnOpenYellowPage.Find(ct).Click() },
            };
        }
    }
}