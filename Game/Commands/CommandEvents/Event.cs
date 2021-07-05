using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Game.Commands.CommandEvents
{
    internal abstract class Event
    {
        public object Data { get; protected set; }
    }
}