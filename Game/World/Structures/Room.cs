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

        public override string ToString()
        {
            return GetType().Name + $" {Coordinates.X}.{Coordinates.Y}";
        }
    }
}
