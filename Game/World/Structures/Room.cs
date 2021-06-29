using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ZuulTextBased.Utility;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.DataStructures;
using ZuulTextBased.Utility.Logging;
using System.Drawing;

namespace ZuulTextBased.Game.World.Structures
{
    internal class Room : Area
    {
        //TODO: add items

        public Point Coordinates { get; private set; }

        public Room(Point coordinates)
        {
            Coordinates = coordinates;
        }

        public override bool RemoveEntity(Entity entity)
        {
            if (Entities.Contains(entity))
            {
                Entities.Remove(entity);
                Limbo.Instance.Enter(entity, true); //Add entity to limbo, prevents null
                Logger.Instance.Info(GetType(), $"Entity {entity.GetType().Name} has left {ToString()}");
                return true;
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"Entity {entity.GetType().Name} does not exist in Room: {ToString()}");
                return false;
            }
        }

        public override string ToString()
        {
            return GetType().Name + $" {Coordinates.X}.{Coordinates.Y}";
        }
    }
}
