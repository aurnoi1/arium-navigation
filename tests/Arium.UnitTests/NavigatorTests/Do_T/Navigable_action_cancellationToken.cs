using Arium.Interfaces;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Arium.UnitTests.NavigatorTests.Do_T
{
    public class Navigable_action_cancellationToken
    {
        public class Given_A_CancellationTokenSource_Of_200_ms
        {
            [Fact]
            public void When_Invoke_A_Task_Of_100_ms_Then_Should_Returns_Before_Cancellation()
            {
                // Arrange
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                var log = fixture.Freeze<ILog>();
                var map = fixture.Freeze<IMap>();
                foreach (var node in fixture.CreateMany<INavigable>())
                {
                    Mock.Get(node).Setup(x => x.PublishStatus().Ready.Value).Returns(true);
                    Mock.Get(node).Setup(x => x.PublishStatus().Exist.Value).Returns(true);
                    map.Nodes.Add(node);
                }

                var navigator = new Navigator(map, log);
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));

                // Act
                navigator.Do<INavigable>(map.Nodes.First(), (ct) =>
                {
                    Thread.Sleep(100);
                    return map.Nodes.First();
                }, globalCTS.Token);

                // Assert
                globalCTS.IsCancellationRequested.Should().BeFalse();
            }

            [Fact]
            public void When_Invoke_A_Task_Of_600_ms_Then_Should_Returns_Throws_OperationCanceledException()
            {
                // Arrange
                var fixture = new Fixture().Customize(new AutoMoqCustomization());
                var log = fixture.Freeze<ILog>();
                var map = fixture.Freeze<IMap>();
                foreach (var node in fixture.CreateMany<INavigable>())
                {
                    Mock.Get(node).Setup(x => x.PublishStatus().Ready.Value).Returns(true);
                    Mock.Get(node).Setup(x => x.PublishStatus().Exist.Value).Returns(true);
                    map.Nodes.Add(node);
                }

                var navigator = new Navigator(map, log);
                using var globalCTS = new CancellationTokenSource(TimeSpan.FromMilliseconds(200));

                // Act
                Action act = () => navigator.Do<INavigable>(map.Nodes.First(), (ct) =>
                {
                    Thread.Sleep(600);
                    return map.Nodes.First();
                }, globalCTS.Token);

                // Assert
                act.Should().Throw<OperationCanceledException>();
                globalCTS.IsCancellationRequested.Should().BeTrue();
            }
        }
    }
}