using System.Collections.Generic;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.DataStructures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    internal abstract class Entrance
    {
        private TwoWayValuePair<Area> OtherArea { get; set; }

        public void SetAreas(Area source, Area destination)
        {
            OtherArea = new TwoWayValuePair<Area>(source, destination);
        }

        public void EntityEntersFrom(Area area, Entity entity)
        {
            OtherArea[area].AddEntity(entity);
        }
    }
}