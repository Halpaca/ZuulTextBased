using System.Collections.Generic;
using ZuulTextBased.Utility;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logging;
using System;

namespace ZuulTextBased.Game.World.Structures
{
    internal abstract class Area
    {
        public Dictionary<Direction, Entrance> Exits { get; private set; }
        public LinkedList<Entity> Entities { get; private set; } //Linkedlist because mid entries can leave, linking the other nodes

        public Area()
        {
            Exits = new Dictionary<Direction, Entrance>();
            Entities = new LinkedList<Entity>();
        }

        public void AddTwoWayExit(Direction direction, Area area, Type EntranceType)
        {
            if(!ExitExists(direction) && !area.ExitExists(Directions.Inverse(direction)))
            {
                AddOneWayExit(direction, area, EntranceType);
                area.AddOneWayExit(Directions.Inverse(direction), this, EntranceType);
            }
        }

        public void AddOneWayExit(Direction direction, Area area, Type EntranceType)
        {
            //TODO: Check if type is Entrance
            if (!ExitExists(direction))
            {
                Entrance entrance = (Entrance)Activator.CreateInstance(EntranceType, this, area);
                Exits.Add(direction, entrance);
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"Entrance already exists in direction {direction}");
            }
        }

        public virtual void Enter(Entity entity)
        {
            Limbo.Instance.Leave(entity);
            Entities.AddLast(entity);
            entity.CurrentArea = this;
            Logger.Instance.Info(GetType(), $"Entity {entity.GetType().Name} has entered {ToString()}");
        }

        public virtual void Leave(Entity entity)
        {
            Entities.Remove(entity);
            Logger.Instance.Info(GetType(), $"Entity {entity.GetType().Name} has left {ToString()}");
            Limbo.Instance.Enter(entity); //Set transistional space in case any error occurs, prevents null
        }

        public bool ExitExists(Direction direction)
        {
            return Exits.ContainsKey(direction);
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}