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

        /// <summary>
        /// Used by entities to travel between areas
        /// </summary>
        public Area NextArea(Entity entity, Direction direction)
        {
            //Moving responsibility to Area for moving entities instead of TwoWayEntrance. To be removed if the new code works
            //Exits[direction].PassTrough(entity, this);
            if (ExitExists(direction) && Exits[direction].IsPassable())
            {
                return Exits[direction].GetDestination(this);
            }
            else
            {
                Logger.Instance.Info(GetType(), $"Entity {entity.GetType().Name} walked towards a non-entrance and bumped their head");
                return this;
            }
        }

        /// <summary>
        /// Used by entrances to move an entity from limbo to this area
        /// </summary>
        public virtual void AddEntity(Entity entity)
        {
            Limbo.Instance.RemoveEntity(entity);
            Entities.AddLast(entity);
            entity.CurrentArea = this;
            Logger.Instance.Info(GetType(), $"Entity {entity.GetType().Name} has entered {ToString()}");
        }

        /// <summary>
        /// Used by entrances to move an entity from this area to limbo, a special case object
        /// Returns true if the entity existed and was removed succesfully
        /// </summary>
        public abstract bool RemoveEntity(Entity entity);

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