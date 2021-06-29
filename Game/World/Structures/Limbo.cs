using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    /// <summary>
    /// Singleton area used as a special case object. 
    /// Its intention is to prevent the use of null or with error handling when an entity doesn't move correctly
    /// </summary>
    internal class Limbo : Area
    {
        public static Limbo Instance { get; } = new Limbo();

        private Limbo() { }

        /// <summary>
        /// Overloaded function. Add true to the bool to let the program know the entity is transistioning between areas
        /// </summary>
        public void AddEntity(Entity entity, bool intentional)
        {
            if(intentional)
            {
                Entities.AddLast(entity);
                Logger.Instance.Debug(GetType(), $"Entity {entity.GetType().Name} has entered limbo (Intentional)");
            }
            else
            {
                AddEntity(entity);
            }
        }

        public override void AddEntity(Entity entity)
        {
            if(!Entities.Contains(entity))
            {
                Logger.Instance.Warn(GetType(), $"Entity {entity.GetType().Name} has entered limbo unintentionally");
                Entities.AddLast(entity);
            }
            else
            {
                Logger.Instance.Debug(GetType(), $"Enity {entity.GetType().Name} was already added to limbo, breaking loop");
            }
            //TODO: add some sort of error handling for the player if they enter limbo due to a bug
        }

        public override bool RemoveEntity(Entity entity)
        {
            if(Entities.Contains(entity))
            {
                Logger.Instance.Debug(GetType(), $"Entity {entity.GetType().Name} has left limbo");
                return Entities.Remove(entity);
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"Entity {entity.GetType().Name} does not exist in limbo");
                return false;
            }
        }
    }
}
