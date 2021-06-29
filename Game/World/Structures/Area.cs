using System.Collections.Generic;
using ZuulTextBased.Utility;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logging;
using System;

namespace ZuulTextBased.Game.World.Structures
{
    internal abstract class Area
    {
        public Dictionary<Direction, TwoWayEntrance> Exits { get; private set; }
        public LinkedList<Entity> Entities { get; private set; } //Linkedlist because mid entries can leave, linking the other nodes

        public Area()
        {
            Exits = new Dictionary<Direction, TwoWayEntrance>();
            Entities = new LinkedList<Entity>();
        }

        public void Update()
        {
            foreach(Entity entity in Entities)
            {
                entity.Update();
            }    
        }

        /// <summary>
        /// Links this area to another area and creates an entrance inbetween them
        /// </summary>
        public void LinkAreas(Direction direction, Area destination, TwoWayEntrance entrance)
        {
            Direction inverseDirection = Directions.Inverse(direction);
            if (!ExitExists(direction) && !destination.ExitExists(inverseDirection))
            {
                entrance.SetAreas(this, destination);
                Exits.Add(direction, entrance);
                destination.Exits.Add(inverseDirection, entrance);
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"Entrance already exists in either {GetType().Name}.{direction} or {destination.GetType().Name}.{inverseDirection}");
            }
        }

        public bool CanMoveTo(Direction direction)
        {
            return ExitExists(direction) && Exits[direction].IsPassable();
        }

        /// <summary>
        /// Used by entities to travel between areas
        /// </summary>
        public Area GetFromDirection(Direction direction)
        {
            return Exits[direction].GetDestination(this);
        }

        public virtual void AddEntity(Entity entity)
        {
            if(!Entities.Contains(entity))
            {
                Entities.AddLast(entity);
                Logger.Instance.Info(GetType(), $"Entity {entity} has entered {ToString()}");
            }
            else
            {
                if(entity.CurrentArea == this)
                {
                    Logger.Instance.Debug(GetType(), $"{ToString()} contains entity, bi-directional association is assumed");
                }
            }
        }

        public virtual bool RemoveEntity(Entity entity)
        {
            if (Entities.Contains(entity))
            {
                Entities.Remove(entity);
                Logger.Instance.Info(GetType(), $"{entity} has left {ToString()}");
                return true;
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"{entity} does not exist in Room: {ToString()}");
                //TODO: consider adding Limbo add here
                return false;
            }
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