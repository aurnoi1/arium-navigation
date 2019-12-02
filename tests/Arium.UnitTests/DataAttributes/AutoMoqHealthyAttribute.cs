using Arium.Interfaces;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;

namespace Arium.UnitTests.DataAttributes
{
    public class AutoMoqHealthyAttribute : AutoDataAttribute
    {
        public AutoMoqHealthyAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var log = fixture.Freeze<ILog>();
            var map = fixture.Freeze<IMap>();
            foreach (var node in fixture.CreateMany<INavigable>())
            {
                Mock.Get(node).Setup(x => x.PublishStatus().Ready.Value).Returns(true);
                Mock.Get(node).Setup(x => x.PublishStatus().Exist.Value).Returns(true);
                map.Nodes.Add(node);
            }

            fixture.Register<INavigator>(() => new Navigator(map, log));
            return fixture;
        })
        {
        }
    }
}