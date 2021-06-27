using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    /// <summary>
    /// Singleton area that as a transitional space between areas.
    /// Used as a failsafe when an entity tries to move between rooms
    /// and as a special case object when something goes wrong with trying to enter an area
    /// </summary>
    internal class Limbo : Area
    {
        public static Limbo Instance { get; } = new Limbo();

        /// <summary>
        /// Overloaded function. Add true to the bool to let the program know the entity is transistioning between areas
        /// </summary>
        public void Enter(Entity entity, bool intentional)
        {
            if(intentional)
            {
                Entities.AddLast(entity);
                entity.CurrentArea = this;
                Logger.Instance.Debug(GetType(), $"Entity {entity.GetType().Name} has entered limbo (Intentional)");
            }
            else
            {
                Enter(entity);
            }
        }

        public override void Enter(Entity entity)
        {
            Logger.Instance.Warn(GetType(), $"Entity {entity.GetType().Name} has entered limbo unintentionally");
            Entities.AddLast(entity);
            entity.CurrentArea = this;
            //TODO: add error handling for the player by adding a portal to the starting room of the current floor
        }

        public override void Leave(Entity entity)
        {
            Entities.Remove(entity);
            Logger.Instance.Debug(GetType(), $"Entity {entity.GetType().Name} has left limbo");
        }
    }
}
