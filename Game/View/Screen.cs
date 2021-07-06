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
        public int X => Borders.X;
        public int Y => Borders.Y;
        public int Width => Borders.Width;
        public int Height => Borders.Height;

        //Subscreens are used to draw extra borders and later have roles of showing info to the player (TODO)
        public LinkedList<Screen> SubScreens { get; private set; }

        //TODO: add a list of potential elements, like a text output element used to write to the output screen for example

        private readonly string _name;

        public Screen(string name, int x, int y, int width, int height)
        {
            _name = name;
            Borders = new Rectangle(x, y, width, height);
            SubScreens = new LinkedList<Screen>();
        }

        /// <summary>
        /// Adds a subscreen as a node to this screen after validation, used to percieve different info to the player
        /// </summary>
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

        /// <summary>
        /// Used for drawing the screen and to determine is the current drawing coordinates are on a part of a border
        /// Currently distinguishes Horizontal, Vertical, and Corners
        /// </summary>
        public BorderType GetBorderTypeAt(Point p)
        {
            return p switch
            {
                _ when IsCorner(p, Borders.X, Borders.Y) => BorderType.TopLeft,
                _ when IsCorner(p, Borders.Width - 1, Borders.Y) => BorderType.TopRight,
                _ when IsCorner(p, Borders.X, Borders.Height - 1) => BorderType.BottomLeft,
                _ when IsCorner(p, Borders.Width - 1, Borders.Height - 1) => BorderType.BottomRight,
                _ when IsVerticalAxis(p, Borders.X) || IsVerticalAxis(p, Borders.Width - 1) => BorderType.Vertical,
                _ when IsHorizotantalAxis(p, Borders.Y) || IsHorizotantalAxis(p, Borders.Height - 1) => BorderType.Horizontal,
                _ => BorderType.None
                //TODO: Add support for sub screens by adding clauses for T-Splits ╦ ╩ ╠ ╣ and Cross Section ╬
            };
        }

        private bool IsVerticalAxis(Point p, int xAxis)
        {
            return p.X == xAxis && Borders.Contains(p);
        }

        private bool IsHorizotantalAxis(Point p, int yAxis)
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
