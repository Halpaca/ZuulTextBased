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
        public LinkedList<Agent> Entities { get; private set; } //Linkedlist because mid entries can leave, linking the other nodes

        public Area()
        {
            Exits = new Dictionary<Direction, TwoWayEntrance>();
            Entities = new LinkedList<Agent>();
        }

        public void Update()
        {
            foreach(Agent entity in Entities)
            {
                entity.Update();
            }    
        }

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

        public void ToNextRoom(Agent entity, Direction direction)
        {
            if (ExitExists(direction))
            {
                Exits[direction].PassTrough(entity, this);
            }
            else
            {
                Logger.Instance.Info(GetType(), $"Entity {entity.GetType().Name} walked towards a non-entrance and bumped their head");
            }
        }

        public virtual void Enter(Agent entity)
        {
            Limbo.Instance.Leave(entity);
            Entities.AddLast(entity);
            entity.CurrentArea = this;
            Logger.Instance.Info(GetType(), $"Entity {entity.GetType().Name} has entered {ToString()}");
        }

        public virtual void Leave(Agent entity)
        {
            Entities.Remove(entity);
            Logger.Instance.Info(GetType(), $"Entity {entity.GetType().Name} has left {ToString()}");
            Limbo.Instance.Enter(entity, true); //Set transistional space in case any error occurs, prevents null
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