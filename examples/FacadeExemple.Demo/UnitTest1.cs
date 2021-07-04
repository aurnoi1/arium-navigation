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
            var testContext = new TestContext(controlTimeOut, navigationCancellationSource.Token);
            using AUT aut = new AUT(testContext);
            using AUT aut2 = new AUT(testContext);
            var pageA = aut.Map.Nodes.Where(x => x.GetType() == typeof(PageA)).SingleOrDefault() as PageA;
            aut.Map.PageA.PublishStatus();
            Assert.True((bool)aut.PageA.PublishStatus().Exist.Value);
            Assert.True((bool)aut.PageA.PublishStatus().Ready.Value);
            Assert.Single(aut.Log.Historic);
            Assert.Same(pageA, aut.PageA);
            Assert.Same(aut.Log, aut.Browser.Log);
            Assert.False(aut.Browser.GlobalCancellationToken.IsCancellationRequested);
            Assert.Same(aut.Log, pageA.Log);
            Assert.Same(aut.Map, pageA.Map);
            Assert.Same(aut.PageB.Log, pageA.Log);
            Assert.Same(aut.PageC.Log, pageA.Log);
            Assert.Equal(controlTimeOut, pageA.ControlTimeout);
            Assert.True(aut.Browser.Exists(aut.PageA));
            Assert.True(aut.Browser.WaitForReady(aut.PageA));
            aut.Browser
                .Goto(aut.PageA)
                .Goto(aut.PageB)
                .Do<PageA>((lt) =>
                {
                    Assert.NotEqual(lt, navigationCancellationSource.Token);
                    return aut.PageB.OpenPageA(lt);
                }, TimeSpan.FromSeconds(30))
                .Do((lt) =>
                {
                    Assert.Equal(lt, navigationCancellationSource.Token);
                })
                .Goto(aut.PageC)
                .Goto(aut.PageA);
        }
    }
}
