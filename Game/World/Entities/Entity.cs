using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.Utility;
using ZuulTextBased.Game.World.Structures;

namespace ZuulTextBased.Game.World.Entities
{
    internal abstract class Entity
    {
        public Area CurrentArea { get; set; }

        public virtual void Move(Direction direction)
        {
            CurrentArea.PassEntity(direction, this);
        }

        public virtual void Enter(Area area)
        {
            area.AddEntity(this);
        }
    }
}
