using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands.CommandEvents;

namespace ZuulTextBased.Commands.CommandEvents
{
    interface ICommandObserver
    {
        public abstract void OnNotify(Event state);
    }
}
