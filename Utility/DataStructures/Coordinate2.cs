using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Utility.DataStructures
{
    internal struct Coordinate2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
