using AutoFixture.Xunit2;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using Propertium.UnitTests.DataAttributes;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TimeoutEx;
using Xunit;

namespace Propertium.UnitTests.WebElementEx.Wait
{
    public class CancellationTokens_
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
                using var defaultCancellationTokenSource = new CancellationTokenSource(100.Milliseconds());
                using var cancellationTokenSource = new CancellationTokenSource(50.Milliseconds());
                var cancellationToken = cancellationTokenSource.Token;
                var expected = webElements.ElementAt(index);
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, index, defaultCancellationTokenSource.Token);

                // Act
                var actual = sut.Get(cancellationToken);

                // Assert
                actual.ShouldBe(expected);
            }

            [Theory, AutoMoqData]
            public void When_index_is_out_of_range_Then_returns_null(
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value)
            {
                // Arrange
                using var defaultCancellationTokenSource = new CancellationTokenSource(100.Milliseconds());
                using var cancellationTokenSource = new CancellationTokenSource(50.Milliseconds());
                var cancellationToken = cancellationTokenSource.Token;
                int indexOutOfRange = webElements.Count + 1;
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, indexOutOfRange, defaultCancellationTokenSource.Token);

                // Act
                var actual = sut.Get(cancellationToken);

                // Assert
                actual.ShouldBeNull();
            }
        }

        public class Given_a_defaultCancellationToken_And_3_webElements_with_same_locator_properties_
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
                using var defaultCancellationTokenSource = new CancellationTokenSource(100.Milliseconds());
                var defaultCancellationToken = defaultCancellationTokenSource.Token;
                using var cancellationTokenSource = new CancellationTokenSource(100.Milliseconds());
                var cancellationToken = cancellationTokenSource.Token;
                var expected = webElements.ElementAt(index);
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, index, defaultCancellationToken);

                // Act
                var actual = sut.Get(cancellationToken);

                // Assert
                actual.ShouldBe(expected);
            }

            [Theory, AutoMoqData]
            public void When_index_is_out_of_range_Then_returns_null(
                IFindsByFluentSelector<IWebElement> webDriver,
                [Frozen]IReadOnlyCollection<IWebElement> webElements,
                string locator,
                string value)
            {
                // Arrange
                using var defaultCancellationTokenSource = new CancellationTokenSource(150.Milliseconds());
                var defaultCancellationToken = defaultCancellationTokenSource.Token;
                using var cancellationTokenSource = new CancellationTokenSource(50.Milliseconds());
                var cancellationToken = cancellationTokenSource.Token;
                int indexOutOfRange = webElements.Count + 1;
                Mock.Get(webDriver).Setup(x => x.FindElements(locator, value)).Returns(webElements);
                var sut = new SearchProperty<IWebElement>(locator, value, webDriver, indexOutOfRange, defaultCancellationToken);

                // Act
                var actual = sut.Get(cancellationToken);

                // Assert
                actual.ShouldBeNull();
            }
        }
    }
}