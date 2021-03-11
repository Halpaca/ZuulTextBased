using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Utility;
using ZuulTextBased.Game.World.Entities.BodyStructure;
using ZuulTextBased.Game.World.Structures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Entities
{
    internal abstract class Agent
    {
        public Body Body { get; private set; }
        public Area CurrentArea { get; set; }

        public Agent()
        {
            Body = new Body();
            CurrentArea = new Limbo();
        }

        public abstract void Update();

        public virtual void Move(Direction direction)
        {
            if(direction != Direction.None)
            {
                if (true) //TODO: use Body.CanMove instead
                {
                    CurrentArea.ToNextRoom(this, direction);
                }
                else
                {
                    //TODO: Write reason why you can't move
                }
            }
        }
    }
}
