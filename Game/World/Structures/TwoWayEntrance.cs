using System.Collections.Generic;
using System.Diagnostics;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.DataStructures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    internal abstract class TwoWayEntrance
    {
        public TwoWayValuePair<Area> Destinations { get; private set; }

        public TwoWayEntrance()
        {
            Destinations = new TwoWayValuePair<Area>(Limbo.Instance, Limbo.Instance);
        }

        public void SetAreas(Area area1, Area area2)
        {
            Destinations.SetValues(area1, area2);
        }

        public virtual void PassTrough(Entity entity, Area source)
        {
            if(IsPassable())
            {
                source.Leave(entity);
                Destinations[source].Enter(entity);
            }
        }

        public abstract bool IsPassable();

        ~TwoWayEntrance()
        {
            Destinations.SetValues(null, null);
        }
    }
}