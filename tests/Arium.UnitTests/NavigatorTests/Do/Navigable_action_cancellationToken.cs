using Arium.Interfaces;
using Arium.UnitTests.DataAttributes;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Arium.UnitTests.NavigatorTests.Do
{
    public class Navigable_action_cancellationToken
    {
        public class Given_A_CancellationTokenSource_Of_200_ms
        {
            [Theory, AutoMoqHealthy]
            public void When_Invoke_A_Task_Of_100_ms_Then_Should_Returns_Before_Cancellation(INavigator navigator)
            {
                // Arrange
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));

                // Act
                navigator.Do(navigator.Map.Nodes.First(), (ct) => Thread.Sleep(100), globalCTS.Token);

                // Assert
                globalCTS.IsCancellationRequested.Should().BeFalse();
            }

            [Theory, AutoMoqHealthy]
            public void When_Invoke_A_Task_Of_600_ms_Then_Should_Returns_Throws_OperationCanceledException(INavigator navigator)
            {
                // Arrange
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));

                // Act
                Action act = () => navigator.Do(navigator.Map.Nodes.First(), (ct) => Thread.Sleep(600), globalCTS.Token);

                // Assert
                act.Should().Throw<OperationCanceledException>();
                globalCTS.IsCancellationRequested.Should().BeTrue();
            }
        }
    }
}