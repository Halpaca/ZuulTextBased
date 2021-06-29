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
            CurrentArea = Limbo.Instance;
            Limbo.Instance.AddEntity(this, true);
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

        public void MoveToArea(Area nextArea)
        {
            CurrentArea.RemoveEntity(this);
            SetArea(nextArea);
        }

        private void SetArea(Area area)
        {
            Logger.Instance.Info(GetType(), $"Player is moving from {CurrentArea} to {area}");
            CurrentArea = area;
            CurrentArea.AddEntity(this);
        }
    }
}
