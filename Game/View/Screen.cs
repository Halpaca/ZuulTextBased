using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.View
{
    /// <summary>
    /// Represents an area which can be drawn to the screen
    /// Uses a tree structure to contain subscreens for different roles within the screen
    /// </summary>
    class Screen
    {
        public Rectangle Borders { get; private set; }
        public int Width => Borders.Width;
        public int Height => Borders.Height;
        public int Top => Borders.Top;
        public int Bottom => Borders.Bottom;
        public int Left => Borders.Left;
        public int Right => Borders.Right;

        //Subscreens are used to draw extra borders and later have roles of showing info to the player (TODO)
        public LinkedList<Screen> SubScreens { get; private set; }

        //TODO: add a list of potential elements, like a text output element used to write to the output screen for example

        public string Name { get; private set; }

        public Screen(string name, int x, int y, int width, int height)
        {
            Name = name;
            Borders = new Rectangle(x, y, width, height);
            SubScreens = new LinkedList<Screen>();
        }

        /// <summary>
        /// Adds a subscreen as a node to this screen after validation, used to percieve different info to the player
        /// </summary>
        public void AddSubScreen(Screen screen)
        {
            screen.Offset(Borders.X, Borders.Y);
            if (SubScreenIsValid(screen))
            {
                SubScreens.AddLast(screen);
                Logger.Instance.Info(GetType(), $"Added screen {screen} as a subscreen to {this}");
            }
            else
            {
                Logger.Instance.Error(GetType(), $"Unable to validate the addition of subscreen {screen}, game will have difficulty running");
            }
        }

        /// <summary>
        /// Validates a subscreen to be added on two criterea:
        /// The subscreen is fully contained within its parent
        /// The subscreen does not intersect with any other subscreens already present
        /// </summary>
        private bool SubScreenIsValid(Screen screen)
        {
            if (!Borders.Contains(screen.Borders))
            {
                return false;
            }
            foreach (Screen subScreen in SubScreens)
            {
                if (subScreen.Borders.IntersectsWith(screen.Borders))
                {
                    return false;
                }
            }
            return true;
        }

        public void Offset(int x, int y)
        {
            Rectangle BorderOffset = Borders;
            BorderOffset.Offset(x, y);
            Borders = BorderOffset;
        }

        public BorderType Test(Point coords)
        {
            BorderType border = FindBorder(coords);
            foreach(Screen subScreen in SubScreens)
            {
                BorderType subBorder = subScreen.FindBorder(coords);
                if(border == BorderType.None && subBorder != BorderType.None)
                {
                    border = subBorder;
                }
            }
            return border;
        }

        private BorderType FindBorder(Point coords)
        {
            if(Contains(coords))
            {
                if (IsOnBorder(coords))
                {
                    return GetBorderTypeAt(coords);
                }
            }
            return BorderType.None;
        }

        /// <summary>
        /// Used for drawing the screen and to determine is the current drawing coordinates are on a part of a border
        /// Currently distinguishes Horizontal, Vertical, and Corners
        /// </summary>
        public BorderType GetBorderTypeAt(Point p)
        {
            return p switch
            {
                _ when p.Y == Borders.Top && p.X == Borders.Left => BorderType.TopLeft,
                _ when p.Y == Borders.Top && p.X == Borders.Right => BorderType.TopRight,
                _ when p.Y == Borders.Bottom && p.X == Borders.Left => BorderType.BottomLeft,
                _ when p.Y == Borders.Bottom && p.X == Borders.Right => BorderType.BottomRight,
                _ when IsLeftOrRight(p) => BorderType.Vertical,
                _ when IsTopOrBottom(p) => BorderType.Horizontal,
                _ => BorderType.None
                //TODO: Add support for sub screens by adding clauses for T-Splits ╦ ╩ ╠ ╣ and Cross Section ╬
            };
        }

        private bool IsOnBorder(Point p)
        {
            return IsTopOrBottom(p) || IsLeftOrRight(p);
        }

        private bool IsLeftOrRight(Point p)
        {
            return (p.X == Borders.Left || p.X == Borders.Right) /*&& Borders.Contains(p)*/;
        }

        private bool IsTopOrBottom(Point p)
        {
            return (p.Y == Borders.Top || p.Y == Borders.Bottom) /*&& Borders.Contains(p)*/;
        }

        private bool Contains(Point p)
        {
            return p.X >= Borders.Left && p.X <= Borders.Right && p.Y >= Borders.Top && p.Y <= Borders.Bottom;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
