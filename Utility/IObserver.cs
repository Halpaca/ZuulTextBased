using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands.CommandEvents;

namespace ZuulTextBased.Utility
{
    interface IObserver
    {
        public abstract void OnNotify(CommandEvent state);
    }
}
