using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ZuulTextBased.Utility;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.DataStructures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    internal class Room : Area
    {
        public Coordinate2 coordinates { get; private set; }

        public Room(int xcoord, int ycoord)
        {
            coordinates = new Coordinate2(xcoord, ycoord);
        }

        public override void Enter(Entity entity)
        {
            base.Enter(entity);
            Logger.Instance.Info("Area", $"Entity {entity.GetType().Name} has entered room at {coordinates}");
        }
    }
}
