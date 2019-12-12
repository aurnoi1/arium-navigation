using AutoFixture.Xunit2;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using Propertium.UnitTests.DataAttributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Propertium.UnitTests.SearchProperties.Exist
{
    public class Default_
    {
        public class Given_the_webElement_exist
        {
            [Theory]
            [InlineAutoMoqData(0)]
            public void When_the_webElement_is_found_Then_returns_True(
                int index,
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value
                )
            {
                // Arrange
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(
                    locator,
                    value,
                    webDriver,
                    index,
                    150.Milliseconds());

                // Act
                var actual = sut.Exist();

                // Assert
                actual.Should().Be(true);
            }
        }

        public class Given_the_webElement_does_not_exist
        {
            [Theory]
            [InlineAutoMoqData(0)]
            public void When_the_webElement_is_not_found_Then_returns_False(
                int index,
                IFindsByFluentSelector<IWebElement> webDriver,
                string locator,
                string value
                )
            {
                // Arrange
                var emptyElementCollection = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(emptyElementCollection);
                var sut = new SearchProperty<IWebElement>(
                    locator,
                    value,
                    webDriver,
                    index,
                    150.Milliseconds());

                // Act
                var actual = sut.Exist();

                // Assert
                actual.Should().Be(false);
            }
        }
    }
}