using System.Collections.Generic;
using ZuulTextBased.Utility;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
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

        public void AddExit(Entrance entrance, Direction direction)
        {
            if (!ExitExists(direction))
            {
                Exits.Add(direction, entrance);
            }
            else
            {
                Logger.Instance.Warn("Area", $"Entrance already exists in direction {direction}");
            }
        }

        public virtual void Enter(Entity entity)
        {
            Entities.AddLast(entity);
            entity.CurrentArea = this;
        }

        public void Leave(Entity entity)
        {
            Entities.Remove(entity);
            entity.CurrentArea = null; //TODO: Maybe use null object pattern? (NullArea)
        }

        public bool ExitExists(Direction direction)
        {
            return Exits.ContainsKey(direction);
        }
    }
}