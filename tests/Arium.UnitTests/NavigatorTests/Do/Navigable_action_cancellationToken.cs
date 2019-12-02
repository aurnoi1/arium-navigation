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
                navigator.Do(navigator.Map.Nodes.First(), (cancellationToken) => Thread.Sleep(100), globalCTS.Token);

                // Assert
                globalCTS.IsCancellationRequested.Should().BeFalse();
            }

            [Theory, AutoMoqHealthy]
            public void When_Invoke_A_Task_Of_400_ms_Then_Should_Returns_Throws_OperationCanceledException(INavigator navigator)
            {
                // Arrange
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));

                // Act
                Action act = () => navigator.Do(navigator.Map.Nodes.First(), (cancellationToken) => Thread.Sleep(400), globalCTS.Token);

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
                Mock.Get(navigator.Map.Nodes.First()).Setup(x => x.PublishStatus().Exist.Value).Returns(false);

                // Act
                Action act = () => navigator.Do(navigator.Map.Nodes.First(), (cancellationToken) => { }, globalCTS.Token);

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
                Mock.Get(navigator.Map.Nodes.Last()).Setup(x => x.PublishStatus().Exist.Value).Returns(true);

                // Act
                navigator.Do(navigator.Map.Nodes.First(), (cancellationToken) => { }, globalCTS.Token);

                // Assert
                globalCTS.IsCancellationRequested.Should().BeFalse();
            }
        }
    }
}