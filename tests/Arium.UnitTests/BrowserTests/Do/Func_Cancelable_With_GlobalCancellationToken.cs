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
    public class Func_Cancelable_With_GlobalCancellationToken
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
                browser.Do<INavigable>((globalCancellationToken) =>
                {
                    Thread.Sleep(100);
                    return navigator.Log.Last;
                });

                // Assert
                globalCTS.IsCancellationRequested.Should().BeFalse();
            }

            [Theory, AutoMoqHealthy]
            public void When_Invoke_A_Task_Of_600_ms_Then_Should_Returns_Throws_OperationCanceledException(INavigator navigator)
            {
                // Arrange
                Mock.Get(navigator.Log).SetupGet(x => x.Last).Returns(navigator.Map.Nodes.First());
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));
                var browser = new Browser(navigator.Map, navigator.Log, navigator, globalCTS.Token);

                // Act
                Action act = () => browser.Do<INavigable>((globalCancellationToken) =>
                {
                    Thread.Sleep(600);
                    return navigator.Log.Last;
                });

                // Assert
                act.Should().Throw<OperationCanceledException>();
                globalCTS.IsCancellationRequested.Should().BeTrue();
            }
        }
    }
}