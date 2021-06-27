using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Commands.CommandEvents
{
    class WriteEvent : Event
    {
        public string Message { get; private set; }

        public WriteEvent(string message)
        {
            Message = message;
        }
    }
}
