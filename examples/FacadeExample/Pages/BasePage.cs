using Arium;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacadeExample.Pages
{
    public abstract class BasePage : Navigable
    {
        public readonly TimeSpan ControlTimeout;
        public readonly ILog Log;
        public readonly IMapFacade Map;

        public BasePage(IMapFacade map, ILog log, TimeSpan controlTimeout)
        {
            Map = map;
            Log = log;
            ControlTimeout = controlTimeout;
            RegisterObserver(log);
        }
    }
}
