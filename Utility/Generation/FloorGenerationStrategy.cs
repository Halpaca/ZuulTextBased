using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.World.Structures;

namespace ZuulTextBased.Utility.Generation
{
    internal abstract class FloorGenerationStrategy
    {
        protected Floor Floor { get; private set; }
        protected Random Random { get; private set; }

        public FloorGenerationStrategy(Floor floor)
        {
            Floor = floor;
            Random = new Random();
        }

        public abstract void GenerateRooms(int amount);
    }
}
