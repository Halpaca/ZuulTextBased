using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Game.Commands.CommandEvents
{
    class WriteEvent : Event
    {
        public WriteEvent(string message)
        {
            Data = message;
        }
    }
}
