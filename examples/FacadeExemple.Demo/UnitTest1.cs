using FacadeExample;
using FacadeExample.Pages;
using System;
using Xunit;

namespace FacadeExemple.Demo
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Map map = new Map();
            var g = map.Nodes;
        }
    }
}
