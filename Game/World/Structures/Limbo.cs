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
            Logger.Instance.Warn(GetType(), $"Entity {entity.GetType().Name} has entered limbo unintentionally, generating portal");
            Entities.AddLast(entity);
            entity.CurrentArea = this;
            throw new NotImplementedException(); //TODO: add error handling by adding a portal to the starting room of the current floor
        }

        public override void Leave(Entity entity)
        {
            Entities.Remove(entity);
            Logger.Instance.Debug(GetType(), $"Entity {entity.GetType().Name} has left limbo");
        }
    }
}
