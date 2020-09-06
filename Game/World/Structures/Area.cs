using System.Collections.Generic;
using ZuulTextBased.Game.Utility;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logger;

namespace ZuulTextBased.Game.World.Structures
{
    internal abstract class Area
    {
        public Dictionary<Direction, Entrance> Exits { get; private set; }
        public LinkedList<Entity> Denizens;

        public Area()
        {
            Exits = new Dictionary<Direction, Entrance>();
            Denizens = new LinkedList<Entity>();
        }

        public void AddEntrance(Entrance entrance, Direction direction)
        {
            if (!EntranceExists(direction))
            {
                Exits.Add(direction, entrance);
            }
            else
            {
                Logger.Instance.Error("Area", $"Entrance already exists in direction {direction}!");
            }
        }

        public void AddEntity(Entity entity)
        {
            Denizens.AddLast(entity);
            entity.CurrentArea = this;
        }

        public void PassEntity(Direction direction, Entity entity)
        {
            if (EntranceExists(direction))
            {
                RemoveEntity(entity);
                Exits[direction].EntityEntersFrom(this, entity);
            }
            else
            {
                Logger.Instance.Info("Area", $"Entity {entity} walked towards a non-entrance and bumped their head");
            }
        }

        public void RemoveEntity(Entity entity)
        {
            Denizens.Remove(entity);
            entity.CurrentArea = null;
        }

        private bool EntranceExists(Direction direction)
        {
            return Exits.ContainsKey(direction);
        }
    }
}