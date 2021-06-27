using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Commands.CommandEvents
{
    internal abstract class Event
    {
        public object data { get; protected set; }
    }
}