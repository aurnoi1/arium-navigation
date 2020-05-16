using FacadeExample;
using FacadeExample.Pages;
using System;
using System.Linq;
using Xunit;

namespace FacadeExemple.Demo
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Map map = new Map();
            var pageA = map.Nodes.Where(x => x.GetType() == typeof(PageA)).SingleOrDefault();
            Assert.Same(pageA, map.PageA);
        }
    }
}
