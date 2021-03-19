using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Generation;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    /// <summary>
    /// Floors are part of the basic dungeon structure and holds a number of linked areas
    /// Default generates with a starting Room at index 0, 0
    /// </summary>
    internal class Floor
    {
        public Dictionary<Point, Area> Areas { get; private set; }
        public FloorGenerationStrategy GenerationStrategy { get; private set; }
        private Random _random;

        public Floor()
        {
            _random = new Random();
            Areas = new Dictionary<Point, Area>();
            CreateRoom(new Point(0, 0));
            GenerationStrategy = new BlobStrategy(this);
        }

        public void Update()
        {
            foreach(Area area in Areas.Values)
            {
                area.Update();
            }
        }

        public void EnterPlayer(Player player)
        {
            AreaAt(new Point(0, 0)).Enter(player);
        }

        public void GenerateRooms(int amount)
        {
            GenerationStrategy.GenerateRooms(amount);
        }

        /// <summary>
        /// Tries to generate a room at the specified coordinates
        /// </summary>
        /// <returns>Returns true if the room was generated succesfully</returns>
        public void CreateRoom(Point coordinates)
        {
            Room newRoom = new Room(coordinates);
            Areas.Add(coordinates, newRoom);
            Logger.Instance.Info(GetType(), $"Generated room at {coordinates.X}.{coordinates.Y}");
        }

        public void LinkAreas(Point source, Direction direction, Point destination)
        {
            AreaAt(source).LinkAreas(direction, AreaAt(destination), new Door()); //TOASK: Does the fail process lead to finalization of Door?
        }

        public Area AreaAt(Point coordinates)
        {
            if (AreaExists(coordinates))
            {
                return Areas[coordinates]; //stucts compare value types instead of object
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"No room found at Coordinates {coordinates.X}.{coordinates.Y}, returning Limbo as special case");
                return Limbo.Instance;
            }
        }

        public bool AreaExists(Point coordinates)
        {
            return Areas.ContainsKey(coordinates);
        }

        public string AsciiMap()
        {
            Rectangle m = GetMapSize();
            string asciiMap = "";
            //TODO: rectangle is upside down, go from bottom to top instead
            for(int i = m.Top; i <= m.Bottom; i++)
            {
                for(int j = m.Left; j <= m.Right; j++)
                {
                    if(AreaExists(new Point(j, i)))
                    {
                        asciiMap += "# ";
                    }
                    else
                    {
                        asciiMap += ". ";
                    }
                }
                asciiMap += "\n";
            }
            return asciiMap;
        }

        private Rectangle GetMapSize()
        {
            int offsetX = 0;
            int offsetY = 0;
            int width = 0;
            int height = 0;
            foreach(Point coordinate in Areas.Keys)
            {
                if(coordinate.X > 0 && coordinate.X > width)
                {
                    width = coordinate.X;
                }
                if(coordinate.X < 0 && coordinate.X < offsetX)
                {
                    offsetX = coordinate.X;
                }
                if (coordinate.Y > 0 && coordinate.Y > height)
                {
                    height = coordinate.Y;
                }
                if (coordinate.Y < 0 && coordinate.Y < offsetY)
                {
                    offsetY = coordinate.Y;
                }
            }
            return new Rectangle(offsetX, offsetY, width + Math.Abs(offsetX), height + Math.Abs(offsetY));
        }
    }
}
