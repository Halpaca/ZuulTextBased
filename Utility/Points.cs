using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ZuulTextBased.Utility
{
    static internal class Points
    {
        public static Point Add(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
    }
}
