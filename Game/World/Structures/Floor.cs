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
            GenerationStrategy = new BlobGenerationStrategy(this);
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
        public Area CreateRoom(Point coordinates)
        {
            if (!AreaExists(coordinates))
            {
                Room newRoom = new Room(coordinates);
                Areas.Add(coordinates, newRoom);
                Logger.Instance.Info(GetType(), $"Generated room at {coordinates.X}.{coordinates.Y}");
                return newRoom;
            }
            else
            {
                Logger.Instance.Info(GetType(), $"Failed to generate room at {coordinates.X}.{coordinates.Y}, area already exists");
                return Limbo.Instance;
            }
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
    }
}
