using Arium;
using Arium.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacadeExample.Pages
{
    public abstract class BasePage : Navigable
    {
        private readonly ILog log;
        private protected BasePage(ILog log)
        {
            this.log = log;
        }
    }
}
