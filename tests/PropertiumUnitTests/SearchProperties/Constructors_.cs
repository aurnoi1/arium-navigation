using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using Propertium.Interfaces;
using Propertium.UnitTests.DataAttributes;
using Shouldly;
using System;
using System.Threading;
using TimeoutEx;
using Xunit;

namespace Propertium.UnitTests.SearchProperties
{
    public class Idioms_
    {
        [Fact]
        public void Verify()
        {
            // Assert
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);
            assertion.Verify(typeof(SearchProperty<IWebElement>).GetConstructors());
        }
    }

    public class Locator_value_webDriver_index_
    {
        [Theory, AutoMoqData]
        public void When_all_parameters_are_valid_Then_returns_SearchProperties(
            int index,
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {
            // Arrange, Act
            var actual = new SearchProperty<IWebElement>(locator, value, webDriver, index, 100.Milliseconds());

            // Assert
            actual.ShouldNotBeNull();
            actual.ShouldBeAssignableTo<ISearchProperty<IWebElement>>();
        }

        [Theory, AutoMoqData]
        public void When_all_parameters_are_valid_Then_assigned_public_properties(
            int index,
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {
            // Arrange, Act
            var actual = new SearchProperty<IWebElement>(locator, value, webDriver, index, 100.Milliseconds());

            // Assert
            actual.Selector.ShouldBe(locator);
            actual.Value.ShouldBe(value);
            actual.WebDriver.ShouldBe(webDriver);
            actual.Index.ShouldBe(index);
        }

        [Theory, AutoMoqData]
        public void When_no_defaultCancellationToken_Then_DefaultCancellationToken_is_None(
            int index,
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {
            // Arrange, Act
            var actual = new SearchProperty<IWebElement>(locator, value, webDriver, index, TimeSpan.Zero);

            // Assert
            actual.DefaultTimeout.ShouldBe(TimeSpan.Zero);
        }
    }

    public class Locator_value_webDriver_index_defaultCancellationToken_
    {
        [Theory, AutoMoqData]
        public void When_all_parameters_are_valid_Then_returns_SearchProperties(
            int index,
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {
            // Act
            var actual = new SearchProperty<IWebElement>(
                locator,
                value,
                webDriver,
                index,
                TimeSpan.Zero);

            // Assert
            actual.ShouldNotBeNull();
            actual.ShouldBeAssignableTo<ISearchProperty<IWebElement>>();
        }

        [Theory, AutoMoqData]
        public void When_all_parameters_are_valid_Then_assigned_public_properties(
            int index,
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {
            // Act
            var actual = new SearchProperty<IWebElement>(
                locator,
                value,
                webDriver,
                index,
                TimeSpan.Zero);

            // Assert
            actual.DefaultTimeout.ShouldBe(TimeSpan.Zero);
            actual.Selector.ShouldBe(locator);
            actual.Value.ShouldBe(value);
            actual.WebDriver.ShouldBe(webDriver);
            actual.Index.ShouldBe(index);
        }
    }

    public class Locator_value_webDriver_defaultCancellationToken_
    {
        [Theory, AutoMoqData]
        public void When_all_parameters_are_valid_Then_returns_SearchProperties(
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {

            // Act
            var actual = new SearchProperty<IWebElement>(
                locator,
                value,
                webDriver,
                50.Milliseconds());

            // Assert
            actual.ShouldNotBeNull();
            actual.ShouldBeAssignableTo<ISearchProperty<IWebElement>>();
        }

        [Theory, AutoMoqData]
        public void When_all_parameters_are_valid_Then_assigned_public_properties(
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {

            // Act
            var actual = new SearchProperty<IWebElement>(
                locator,
                value,
                webDriver,
                50.Milliseconds());

            // Assert
            actual.DefaultTimeout.ShouldBe(50.Milliseconds());
            actual.Selector.ShouldBe(locator);
            actual.Value.ShouldBe(value);
            actual.WebDriver.ShouldBe(webDriver);
        }

        [Theory, AutoMoqData]
        public void When_no_index_is_defined_Then_Index_is_0(
             IFindsByFluentSelector<IWebElement> webDriver,
             string locator,
             string value)
        { 
            // Act
            var actual = new SearchProperty<IWebElement>(
                locator,
                value,
                webDriver,
                50.Milliseconds());

            // Assert
            actual.Index.ShouldBe(0);
        }
    }

    public class Locator_value_webDriver_
    {
        [Theory, AutoMoqData]
        public void When_all_parameters_are_valid_Then_returns_SearchProperties(
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {
            // Arrange, Act
            var actual = new SearchProperty<IWebElement>(
                locator,
                value,
                webDriver,
                100.Milliseconds());

            // Assert
            actual.ShouldNotBeNull();
            actual.ShouldBeAssignableTo<ISearchProperty<IWebElement>>();
        }

        [Theory, AutoMoqData]
        public void When_all_parameters_are_valid_Then_assigned_public_properties(
            IFindsByFluentSelector<IWebElement> webDriver,
            string locator,
            string value)
        {
            // Arrange, Act
            var actual = new SearchProperty<IWebElement>(
                locator,
                value,
                webDriver,
                100.Milliseconds());

            // Assert
            actual.Selector.ShouldBe(locator);
            actual.Value.ShouldBe(value);
            actual.WebDriver.ShouldBe(webDriver);
            actual.Index.ShouldBe(0);
        }

        [Theory, AutoMoqData]
        public void When_no_index_is_defined_Then_Index_is_0(
             IFindsByFluentSelector<IWebElement> webDriver,
             string locator,
             string value)
        {
            // Arrange, Act
            var actual = new SearchProperty<IWebElement>(
                locator,
                value,
                webDriver,
                100.Milliseconds());

            // Assert
            actual.Index.ShouldBe(0);
        }
    }
}