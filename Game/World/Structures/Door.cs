using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Game.World.Structures
{
    internal class Door : TwoWayEntrance
    {
        private Lock _lock;

        public Door(Lock doorLock = null)
        {
            _lock = doorLock;
        }

        public override bool IsPassable()
        {
            //TODO: make lockable, collapsable, broken, etc.
            return true;
        }
    }
}
