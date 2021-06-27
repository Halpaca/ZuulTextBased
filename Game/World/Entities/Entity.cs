using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Utility;
using ZuulTextBased.Game.World.Entities.BodyStructure;
using ZuulTextBased.Game.World.Structures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Entities
{
    internal abstract class Entity
    {
        public Body Body { get; private set; }
        public Area CurrentArea { get; set; }

        public Entity()
        {
            Body = new Body();
            CurrentArea = new Limbo();
        }

        public abstract void Update();

        public virtual void Move(Direction direction)
        {
            if(direction != Direction.None)
            {
                //TODO: use Body.CanMove instead
                CurrentArea.ToNextArea(this, direction);
            }
        }
    }
}
