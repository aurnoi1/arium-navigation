using AUT.Facade;
using AUT.Facade.POMs;
using FluentAssertions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using TimeoutEx;
using Xunit;

namespace Arium.UITests
{
    public class UnitTest1 : IDisposable
    {
        public Uri Uri { get => new Uri("http://localhost:4723/wd/hub"); }

        public AppiumOptions AppiumOptions
        {
            get
            {
                string appFullPath = GetTargetFullPath();
                AppiumOptions appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, appFullPath);
                appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");
                return appiumOptions;
            }
        }

        public WindowsDriver<WindowsElement> WinDriver => new WindowsDriver<WindowsElement>(
                    Uri,
                    AppiumOptions,
                    TimeSpan.FromSeconds(300));

        private string GetTargetFullPath()
        {
            var splited = Environment.CurrentDirectory.Split('\\').ToList();
            var build = splited.ElementAt(splited.IndexOf("bin") + 1);
            var testsDir = Environment.CurrentDirectory.Replace($@"AriumUITests\bin\{build}\netcoreapp3.0", "");
            string path = $@"{testsDir}\AUT\bin\{build}\AUT.exe";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The application under tests \"{path}\" was not found.");
            }

            return path;
        }

        private AppiumLocalService service;

        public UnitTest1()
        {
            //StartAppiumService();
        }

        [Fact]
        public void Test1()
        {
            using var globalCancellationTokenSource = new CancellationTokenSource(10.m());
            var globalCancellationToken = globalCancellationTokenSource.Token;
            var log = new Log();
            var map = new Map<WindowsDriver<WindowsElement>>(WinDriver, log, globalCancellationToken);
            var nav = new Navigator(map, log);
            var browser = new Browser(map, log, nav, globalCancellationToken);
            browser.Navigator.WaitForExist(map.PomMenu, globalCancellationToken);
            browser.Navigator.GoTo(map.PomMenu, map.PomYellow, globalCancellationToken);
            browser.Navigator.GoTo(map.PomYellow, map.PomRed, globalCancellationToken);
            browser.Navigator.GoTo(map.PomRed, map.PomBlue, globalCancellationToken);
            browser.Navigator.GoTo(map.PomBlue, map.PomYellow, globalCancellationToken);
            browser.Navigator.GoTo(map.PomYellow, map.PomRed, globalCancellationToken);
            browser.Navigator.GoTo(map.PomRed, map.PomYellow, globalCancellationToken);
            browser.Navigator.GoTo(map.PomYellow, map.PomYellow, globalCancellationToken);
            browser.Navigator.GoTo(browser.Log.Last, map.PomMenu, globalCancellationToken);
            browser.Navigator.GoTo(map.PomMenu, map.PomYellow, globalCancellationToken);
            browser.Navigator.Do<PomMenu<WindowsDriver<WindowsElement>>>(map.PomYellow, (x) => map.PomYellow.OpenMenuByMenuBtn(TimeSpan.FromSeconds(10)), globalCancellationToken);
            browser.Navigator.Back(globalCancellationToken);
            map.RemoteDriver.Close();
        }

        private void StartAppiumService()
        {
            service = AppiumLocalService.BuildDefaultService();
            service.Start();
        }

        [Fact]
        public void FullExample()
        {
            // Set the GlobalCancellationToken used for the time of the Navigation session.
            using var globalCancellationTokenSource = new CancellationTokenSource(1.m());
            var globalCancellationToken = globalCancellationTokenSource.Token;
            var log = new Log();
            var map = new Map<WindowsDriver<WindowsElement>>(WinDriver, log, globalCancellationToken);
            var navigator = new Navigator(map, log);
            var browser = new Browser(map, log, navigator, globalCancellationToken);
            browser.WaitForExist(map.PomMenu); // Use GlobalCancellationToken.
            browser.WaitForReady(map.PomMenu, 3.s()); // Use timeout in concurrence of GlobalCancellationToken.
            browser
                .GoTo(map.PomYellow)
                .Do<PomMenu<WindowsDriver<WindowsElement>>>(() =>
                {
                    // Add a timeout in concurence of GlobalCancellationToken
                    // Since the same token was injected to Map and Browser.
                    return map.PomYellow.OpenMenuByMenuBtn(3.s());
                })
                .GoTo(map.PomBlue) // Force the path to PomBlue then PomYellow...
                .GoTo(map.PomYellow) //... to test PomYellow.ActionToOpenViewMenu().
                .GoTo(map.PomMenu) // Since last was PomBlue, PomYellow.OpenViewMenuByMenuBtn() will be called to go to ViewMenu.
                .Do(() =>
                {
                    map.PomMenu.EnterText("This is a test");
                })
                .Do((linkedTokens) => // Add a timeout (3s) that will run in concurrence of GlobalCancellationToken.
                {
                    // Keep in mind that Do() will consume the linkedTokens to wait for the page to be ready,
                    // and ensures its existance once all actions have been invoked.
                    // So use a specific CancellationToken if you need more precision in the action cancellation.
                    map.PomMenu.UITitle.Get(linkedTokens).Should().NotBeNull();
                }, 3.s())
                .GoTo(map.PomBlue, 5.s()) // Add a timeout that will run in concurrence of GlobalCancellationToken.
                .Back() // ViewBlue.
                .GoTo(browser.Log.Historic.ElementAt(1)) // The second element of historic is ViewYellow.
                .GoTo(map.PomRed);// Auto resolution of path to red via PomYellow.GetDynamicNeighbors().

            // First page in historic was PomMenu.
            browser.Log.Historic.First().Should().Be(map.PomMenu);
            browser.Log.Historic.Last().Should().Be(map.PomRed);
            map.RemoteDriver.Close();
        }

        public void Dispose()
        {
            //service.Dispose();
        }
    }
}