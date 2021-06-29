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
        public Area CurrentArea { get; private set; }

        public Entity()
        {
            Body = new Body();
            SetArea(Limbo.Instance);
        }

        public abstract void Update();

        public virtual void Move(Direction direction)
        {
            //TODO: add Body.CanMove
            if (direction != Direction.None)
            {
                if(CurrentArea.CanMoveTo(direction))
                {
                    MoveToArea(CurrentArea.GetFromDirection(direction));
                }
                else
                {
                    Logger.Instance.Info(GetType(), $"Entity {GetType().Name} walked towards a non-entrance and bumped their head");
                }
            }
        }

        private void MoveToArea(Area nextArea)
        {
            CurrentArea.RemoveEntity(this);
            SetArea(nextArea);
        }

        public void SetArea(Area area)
        {
            CurrentArea = area;
            CurrentArea.AddEntity(this);
        }
    }
}
