using System.Collections.Generic;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.DataStructures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    internal abstract class Entrance
    {
        public Area Source { get; private set; }
        public Area Destination { get; private set; }
        //private TwoWayValuePair<Area> Destinations { get; set; }

        public Entrance(Area source, Area destination)
        {
            Source = source;
            Destination = destination;
        }

        public virtual void PassTrough(Entity entity)
        {
            Source.Leave(entity);
            Destination.Enter(entity);
        }
    }
}