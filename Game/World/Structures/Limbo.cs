using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    /// <summary>
    /// Singleton area that as a transitional space between areas, staying in limbo is not intentional (yet)
    /// Maybe to be changed to plane of existence later
    /// </summary>
    internal class Limbo : Area
    {
        public static Limbo Instance { get; } = new Limbo();
        private bool _intentional = false; //TODO: If player unintentionally entered limbo (dueto a bug), spawn one way portal to starting room

        public override void Enter(Entity entity)
        {
            Entities.AddLast(entity);
            entity.CurrentArea = this;
            Logger.Instance.Debug(GetType(), $"Entity {entity.GetType().Name} has entered limbo, awaiting leave...");
        }

        public override void Leave(Entity entity)
        {
            Entities.Remove(entity);
            Logger.Instance.Debug(GetType(), $"Entity {entity.GetType().Name} has left limbo as intended");
        }
    }
}
