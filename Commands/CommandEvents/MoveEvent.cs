using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.Utility;

namespace ZuulTextBased.Commands.CommandEvents
{
    class MoveEvent : CommandEvent
    {
        public Direction Direction { get; private set; }

        public MoveEvent(Direction direction)
        {
            Direction = direction;
        }
    }
}
