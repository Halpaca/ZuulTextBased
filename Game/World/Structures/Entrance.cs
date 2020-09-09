using System.Collections.Generic;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.DataStructures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    internal abstract class Entrance
    {
        private TwoWayValuePair<Area> Destinations { get; set; }

        public Entrance(Area source, Area destination)
        {
            Destinations = new TwoWayValuePair<Area>(source, destination);
        }

        public virtual void PassTrough(Area source, Entity entity)
        {
            source.Leave(entity);
            Destinations[source].Enter(entity);
        }
    }
}