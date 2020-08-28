using System.Collections.Generic;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility.Logger;

namespace ZuulTextBased.Game.World
{
    internal abstract class Entrance
    {
        private KeyValuePair<Area, Area> Entryway { get; set; }

        public void SetAreas(Area source, Area destination)
        {
            Entryway = new KeyValuePair<Area, Area>(source, destination);
        }

        public void EnterFrom(Area area, Entity entity)
        {
            if(Entryway.Key == area)
            {
                EnterTo(Entryway.Value, entity);
            }
            else if(Entryway.Value == area)
            {
                EnterTo(Entryway.Key, entity);
            }
            else
            {
                Logger.Instance.Error("Entrance", $"{area} is invalid! It should be {Entryway.Key} or {Entryway.Value}");
            }
        }

        private void EnterTo(Area area, Entity entity)
        {
            area.Enter(entity);
        }
    }
}