using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Utility;

namespace ZuulTextBased.Game.Commands.CommandEvents
{
    class MoveEvent : Event
    {
        public MoveEvent(Direction direction)
        {
            Data = direction;
        }
    }
}
