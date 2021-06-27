using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Utility;

namespace ZuulTextBased.Commands.CommandEvents
{
    class MoveEvent : Event
    {
        //public Direction Direction { get; private set; }

        public MoveEvent(Direction direction)
        {
            data = direction;
        }
    }
}
