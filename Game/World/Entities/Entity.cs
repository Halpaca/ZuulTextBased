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
            CurrentArea = new NullArea();
        }

        public virtual void Move(Direction direction)
        {
            if(true) //TODO: use Body.CanMove instead
            {
                LeaveCurrentArea(direction);
            }
            else
            {
                //TODO: Write reason why you can't move
            }
        }

        public void LeaveCurrentArea(Direction direction)
        {
            if (CurrentArea.ExitExists(direction))
            {
                CurrentArea.Exits[direction].PassTrough(CurrentArea, this);
            }
            else
            {
                Logger.Instance.Info("Entity", $"Entity {GetType().Name} walked towards a non-entrance and bumped their head");
                //TODO: new write event?
            }
        }

        public void Enter(Area area)
        {
            area.Enter(this);
        }
    }
}
