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
            using AUT aut = new AUT(controlTimeOut, navigationCancellationSource.Token);
            using AUT aut2 = new AUT(controlTimeOut, navigationCancellationSource.Token);
            var pageA = aut.Map.Nodes.Where(x => x.GetType() == typeof(PageA)).SingleOrDefault() as PageA;
            aut.Map.PageA.PublishStatus();
            Assert.True((bool)aut.Map.PageA.PublishStatus().Exist.Value);
            Assert.True((bool)aut.Map.PageA.PublishStatus().Ready.Value);
            Assert.Single(aut.Log.Historic);
            Assert.Same(pageA, aut.Map.PageA);
            Assert.Same(aut.Log, aut.Browser.Log);
            Assert.False(aut.Browser.GlobalCancellationToken.IsCancellationRequested);
            Assert.Same(aut.Log, pageA.Log);
            Assert.Same(aut.Map, pageA.Map);
            Assert.Same(aut.Map.PageB.Log, pageA.Log);
            Assert.Same(aut.Map.PageC.Log, pageA.Log);
            Assert.Equal(controlTimeOut, pageA.ControlTimeout);
            Assert.True(aut.Browser.Exists(aut.Map.PageA));
            Assert.True(aut.Browser.WaitForReady(aut.Map.PageA));
        }
    }
}
