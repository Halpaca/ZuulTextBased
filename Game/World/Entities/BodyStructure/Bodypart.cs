using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Game.World.Entities.BodyStructure
{
    internal class Bodypart
    {
        public BodypartType Type { get; private set; }
        //Used to keep track of this body parts statuses and their severities

        public bool IsVital { get; set; }

        public Bodypart(BodypartType type)
        {
            Type = type;
        }
    }
}
