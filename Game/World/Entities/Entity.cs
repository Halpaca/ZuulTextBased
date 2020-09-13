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

        public virtual void Move(Direction direction)
        {
            if(true) //TODO: use Body.CanMove instead
            {
                if(CurrentArea.ExitExists(direction))
                {
                    CurrentArea.Exits[direction].PassTrough(this);
                }
                else
                {
                    Logger.Instance.Info(GetType(), $"Entity {GetType().Name} walked towards a non-entrance and bumped their head");
                }
            }
            else
            {
                //TODO: Write reason why you can't move
            }
        }
    }
}
