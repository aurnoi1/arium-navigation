﻿using AutoFixture.Xunit2;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using Propertium.UnitTests.DataAttributes;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TimeoutEx;
using Xunit;

namespace Propertium.UnitTests.SearchProperties.Find
{
    public class Timeout_
    {
        public class Given_3_webElements_with_same_locator_properties_
        {
            [Theory]
            [InlineAutoMoqData(0)]
            [InlineAutoMoqData(1)]
            [InlineAutoMoqData(2)]
            public void When_index_is_between_0_and_2_Then_returns_webElement_at_matching_index(
                int index,
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value
                )
            {
                // Arrange
                var expected = webElements.ElementAt(index);
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, index, 150.Milliseconds());
                var timeout = 100.Milliseconds();

                // Act
                var actual = sut.Find(timeout);

                // Assert
                actual.ShouldBe(expected);
            }

            [Theory, AutoMoqData]
            public void When_index_is_out_of_range_Then_throws_TimeoutException(
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value)
            {
                // Arrange
                var timeout = 100.Milliseconds();
                int indexOutOfRange = webElements.Count + 1;
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, indexOutOfRange, 150.Milliseconds());

                // Assert
                Assert.Throws<TimeoutException>(() => sut.Find(timeout));
            }

            [Theory, AutoMoqData]
            public void When_index_is_not_defined_Then_returns_first_WebElement(
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value)
            {
                // Arrange
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, 150.Milliseconds());
                var timeout = 100.Milliseconds();

                // Act
                var actual = sut.Find(timeout);

                // Assert
                actual.ShouldBe(webElements.First());
            }
        }

        public class Given_a_defaultCancelationToken_And_3_webElements_with_same_locator_properties_
        {
            [Theory]
            [InlineAutoMoqData(0)]
            [InlineAutoMoqData(1)]
            [InlineAutoMoqData(2)]
            public void When_index_is_between_0_and_2_Then_returns_webElement_at_matching_index(
                int index,
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value
                )
            {
                // Arrange
                var expected = webElements.ElementAt(index);
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, index, 150.Milliseconds());
                var timeout = 100.Milliseconds();

                // Act
                var actual = sut.Find(timeout);

                // Assert
                actual.ShouldBe(expected);
            }

            [Theory, AutoMoqData]
            public void When_index_is_out_of_range_Then_throws_TimeoutException(
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value)
            {
                // Arrange
                var timeout = 100.Milliseconds();
                int indexOutOfRange = webElements.Count + 1;
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, indexOutOfRange, 150.Milliseconds());

                // Assert
                Assert.Throws<TimeoutException>(() => sut.Find(timeout));
            }

            [Theory, AutoMoqData]
            public void When_index_is_not_defined_Then_returns_first_WebElement(
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value)
            {
                // Arrange
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, 150.Milliseconds());
                var timeout = 100.Milliseconds();

                // Act
                var actual = sut.Find(timeout);

                // Assert
                actual.ShouldBe(webElements.First());
            }
        }
    }
}