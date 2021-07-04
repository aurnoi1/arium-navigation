using System;
using System.Threading;

namespace FacadeExample
{
    public class TestContext
    {
        public readonly TimeSpan FindControlTimeout;
        public readonly CancellationToken NavigationCancellation;


        public TestContext(TimeSpan findControlTimeout, CancellationToken navigationCancellation)
        {
            FindControlTimeout = findControlTimeout;
            NavigationCancellation = navigationCancellation;
        }
    }
}
