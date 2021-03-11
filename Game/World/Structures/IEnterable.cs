using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.World.Entities;

namespace ZuulTextBased.Game.World.Structures
{
    /// <summary>
    /// Interface for all enterable classes
    /// Room, Entrance, Portal, Hatch, Etc.
    /// </summary>
    interface IEnterable
    {
        public abstract void Enter(Agent entity);
    }
}
