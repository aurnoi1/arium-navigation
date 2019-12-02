using Arium.Interfaces;
using Arium.UnitTests.DataAttributes;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Arium.UnitTests.BrowserTests.Do
{
    public class Func_Uncancelable_With_GlobalCancellationToken
    {
        public class Given_A_CancellationTokenSource_Of_200_ms
        {
            [Theory, AutoMoqHealthy]
            public void When_Invoke_A_Task_Of_100_ms_Then_Should_Returns_Before_Cancellation(INavigator navigator)
            {
                // Arrange
                Mock.Get(navigator.Log).SetupGet(x => x.Last).Returns(navigator.Map.Nodes.First());
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));
                var browser = new Browser(navigator.Map, navigator.Log, navigator, globalCTS.Token);

                // Act
                browser.Do<INavigable>(() =>
                {
                    Thread.Sleep(100);
                    return navigator.Log.Last;
                });

                // Assert
                globalCTS.IsCancellationRequested.Should().BeFalse();
            }

            [Theory, AutoMoqHealthy]
            public void When_Invoke_A_Task_Of_400_ms_Then_Should_Returns_Throws_OperationCanceledException(INavigator navigator)
            {
                // Arrange
                Mock.Get(navigator.Log).SetupGet(x => x.Last).Returns(navigator.Map.Nodes.First());
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));
                var browser = new Browser(navigator.Map, navigator.Log, navigator, globalCTS.Token);

                // Act
                Action act = () => browser.Do<INavigable>(() =>
                {
                    Thread.Sleep(400);
                    return navigator.Log.Last;
                });

                // Assert
                act.Should().Throw<OperationCanceledException>();
                globalCTS.IsCancellationRequested.Should().BeTrue();
            }
        }

        public class Given_The_Expected_Returned_Navigable_Does_Not_Exist
        {
            [Theory, AutoMoqHealthy]
            public void When_Invoke_A_Task_Then_Should_Returns_Throws_OperationCanceledException(INavigator navigator)
            {
                // Arrange
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));
                Mock.Get(navigator.Log).SetupGet(x => x.Last).Returns(navigator.Map.Nodes.First());
                Mock.Get(navigator.Map.Nodes.Last()).Setup(x => x.PublishStatus().Exist.Value).Returns(false);
                var browser = new Browser(navigator.Map, navigator.Log, navigator, globalCTS.Token);

                // Act
                Action act = () => browser.Do<INavigable>((cancellationToken) =>
                {
                    return browser.Map.Nodes.Last();
                });

                // Assert
                act.Should().Throw<OperationCanceledException>();
                globalCTS.IsCancellationRequested.Should().BeTrue();
            }
        }

        public class Given_The_Expected_Returned_Navigable_Does_Exist
        {
            [Theory, AutoMoqHealthy]
            public void When_Invoke_A_Task_Then_Should_Returns_Before_Cancellation(INavigator navigator)
            {
                // Arrange
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));
                Mock.Get(navigator.Log).SetupGet(x => x.Last).Returns(navigator.Map.Nodes.First());
                Mock.Get(navigator.Map.Nodes.Last()).Setup(x => x.PublishStatus().Exist.Value).Returns(true);
                var browser = new Browser(navigator.Map, navigator.Log, navigator, globalCTS.Token);

                // Act
                browser.Do<INavigable>((cancellationToken) =>
                {
                    return navigator.Map.Nodes.Last();
                });

                // Assert
                globalCTS.IsCancellationRequested.Should().BeFalse();
            }
        }
    }
}