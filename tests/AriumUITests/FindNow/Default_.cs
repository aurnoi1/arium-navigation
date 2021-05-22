using AUT.Facade;
using FluentAssertions;
using OpenQA.Selenium;
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

namespace Arium.UITests.FindNow
{
    [Collection("UITests")]
    public class Default_
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
            var testsDir = Environment.CurrentDirectory.Replace($@"AriumUITests\bin\{build}\netcoreapp3.1", "");
            string path = $@"{testsDir}AUT\bin\{build}\AUT.exe";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The application under tests \"{path}\" was not found.");
            }

            return path;
        }


        [Fact]
        public void Given_The_Element_Exist_Then_Should_Returns_The_Element()
        {
            // Arrange
            using var globalCancellationTokenSource = new CancellationTokenSource(1.m());
            var globalCancellationToken = globalCancellationTokenSource.Token;
            var log = new Log();
            var map = new Map<WindowsDriver<WindowsElement>>(WinDriver, log);
            var navigator = new Navigator(map, log);
            var browser = new Browser(log, navigator, globalCancellationToken);
            browser.WaitForExist(map.PomMenu);

            // Act
            var actual = map.PomMenu.UIBtnOpenBluePage.FindNow();

            // Assert
            actual.Should().NotBeNull();
            actual.Text.Should().Be("Open Blue");

            map.RemoteDriver.CloseApp();
        }

        [Fact]
        public void Given_The_Element_Does_Not_Exist_Then_Should_Returns_Throws_WebDriverException()
        {
            // Arrange
            using var globalCancellationTokenSource = new CancellationTokenSource(1.m());
            var globalCancellationToken = globalCancellationTokenSource.Token;
            var log = new Log();
            var map = new Map<WindowsDriver<WindowsElement>>(WinDriver, log);
            var navigator = new Navigator(map, log);
            var browser = new Browser(log, navigator, globalCancellationToken);
            browser.WaitForExist(map.PomMenu);

            // Act
            Action act = () => map.PomMenu.UIBtnNotImplemented.FindNow();

            // Assert
            act.Should().Throw<WebDriverException>();

            map.RemoteDriver.CloseApp();
        }
    }
}