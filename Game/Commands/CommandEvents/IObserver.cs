using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.Commands.CommandEvents;

namespace ZuulTextBased.Game.Commands.CommandEvents
{
    interface IObserver
    {
        public abstract void OnNotify(Event state);
    }
}
