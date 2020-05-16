using FacadeExample;
using FacadeExample.Pages;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace FacadeExemple.Demo
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var controlTimeOut = TimeSpan.FromSeconds(3);
            using var navigationCancellationSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            AUT aut = new AUT(controlTimeOut, navigationCancellationSource.Token);
            var pageA = aut.Map.Nodes.Where(x => x.GetType() == typeof(PageA)).SingleOrDefault() as PageA;
            Assert.Same(pageA, aut.PageA);
            Assert.Same(aut.Log, aut.Browser.Navigator.Log);
            Assert.Same(aut.Map, aut.Browser.Navigator.Map);
            Assert.False(aut.Browser.GlobalCancellationToken.IsCancellationRequested);
            Assert.Same(aut.Log, pageA.Log);
            Assert.Equal(controlTimeOut, pageA.ControlTimeout);
            Assert.True(aut.PageA.Exist().Invoke());
            Assert.True(aut.PageA.Ready().Invoke());
            aut.PageA.PublishStatus();
        }
    }
}
