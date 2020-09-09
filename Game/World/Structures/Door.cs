using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Game.World.Structures
{
    internal class Door : Entrance
    {
        public Door(Area source, Area destination) : base(source, destination)
        {
        }

        //can be locked
    }
}
