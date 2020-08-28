using System.Collections.Generic;
using ZuulTextBased.Game.Utility;
using ZuulTextBased.Game.World.Entities;

namespace ZuulTextBased.Game.World
{
    internal abstract class Area
    {
        public Dictionary<Direction, Entrance> Exits { get; private set; }
        public LinkedList<Entity> Entities;

        public Area()
        {
            Exits = new Dictionary<Direction, Entrance>();
            Entities = new LinkedList<Entity>();
        }

        public void Enter(Entity e)
        {
            Entities.AddLast(e);
        }

        public void Leave(Direction direction, Entity entity)
        {
            if (Exits.ContainsKey(direction))
            {
                Exits[direction].EnterFrom(this, entity);
            }
        }
    }
}