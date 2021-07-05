using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.View
{
    class Screen
    {
        /// <summary>
        /// Sets borders to be used relatively by any parent screes
        /// </summary>
        public Rectangle Borders { get; private set; }
        public int X => Borders.X;
        public int Y => Borders.Y;
        public int Width => Borders.Width;
        public int Height => Borders.Height;
        public LinkedList<Screen> SubScreens { get; private set; }

        private readonly string _name;

        public Screen(string name, int x, int y, int width, int height)
        {
            _name = name;
            Borders = new Rectangle(x, y, width, height);
            SubScreens = new LinkedList<Screen>();
        }

        public void AddSubScreen(Screen s)
        {
            if(SubScreenIsValid(s))
            {
                SubScreens.AddLast(s);
                Logger.Instance.Info(GetType(), $"Added screen {s} as a subscreen to {this}");
            }
            else
            {
                Logger.Instance.Error(GetType(), $"Unable to validate the addition of subscreen {s}, game will have difficulty running");
            }
        }

        /// <summary>
        /// Validates a subscreen to be added on two criterea:
        /// The subscreen is fully contained within its parent
        /// The subscreen does not intersect with any other subscreens already present
        /// </summary>
        private bool SubScreenIsValid(Screen s)
        {
            if (!Borders.Contains(s.Borders))
            {
                return false;
            }
            foreach (Screen subScreen in SubScreens)
            {
                if (subScreen.Borders.IntersectsWith(s.Borders))
                {
                    return false;
                }
            }
            return true;
        }

        public BorderType GetBorderTypeAt(Point p)
        {
            return p switch
            {
                _ when IsCorner(p, Borders.X, Borders.Y) => BorderType.TopLeft,
                _ when IsCorner(p, Borders.Width - 1, Borders.Y) => BorderType.TopRight,
                _ when IsCorner(p, Borders.X, Borders.Height - 1) => BorderType.BottomLeft,
                _ when IsCorner(p, Borders.Width - 1, Borders.Height - 1) => BorderType.BottomRight,
                _ when IsXBorder(p, Borders.X) => BorderType.Left,
                _ when IsXBorder(p, Borders.Width - 1) => BorderType.Right,
                _ when IsYBorder(p, Borders.Y) => BorderType.Top,
                _ when IsYBorder(p, Borders.Height - 1) => BorderType.Bottom,
                _ => BorderType.Invalid
            };
        }

        private bool IsXBorder(Point p, int xAxis)
        {
            return p.X == xAxis && Borders.Contains(p);
        }

        private bool IsYBorder(Point p, int yAxis)
        {
            return p.Y == yAxis && Borders.Contains(p);
        }

        private bool IsCorner(Point p, int xCoord, int yCoord)
        {
            return p.X == xCoord && p.Y == yCoord && Borders.Contains(p);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
