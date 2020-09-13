using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ZuulTextBased.Commands;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    internal class Dungeon
    {
        private Dictionary<Point, Area> _floor;
        private Random _random;

        public Dungeon()
        {
            _random = new Random();
            _floor = new Dictionary<Point, Area>();
            Point startingCoordinates = new Point(0, 0);
            _floor.Add(startingCoordinates, new Room(startingCoordinates));
        }

        public void AddToStartingArea(Player player)
        {
            AreaAt(0, 0).Enter(player);
        }

        public void GenerateRooms(int Amount)
        {
            while(Amount > 0)
            {
                Point source = RandomExistingCoordinate();
                Direction randomDirection = Directions.RandomValid();
                Point destination = Points.Add(source, Directions.ToAdditivePoint(randomDirection));
                if(GenerateRoom(destination))
                {
                    AreaAt(source).AddTwoWayExit(randomDirection, AreaAt(destination), typeof(Door));
                    Amount--;
                }
            }
        }

        /// <summary>
        /// Tries to generate a room at a specified coordinate
        /// </summary>
        /// <returns>Returns true if the room was generated succesfully</returns>
        private bool GenerateRoom(Point coordinate)
        {
            if(!AreaExists(coordinate))
            {
                _floor.Add(coordinate, new Room(coordinate));
                Logger.Instance.Info(GetType(), $"Generated room at {coordinate.X}.{coordinate.Y}");
                return true;
            }
            else
            {
                Logger.Instance.Info(GetType(), $"Failed to generate room at {coordinate.X}.{coordinate.Y}, area already exists");
                return false;
            }
        }

        private void LinkAreas(Area source, Direction direction, Entrance entrance, Area destination)
        {
            source.AddTwoWayExit(direction, destination, entrance.GetType());
        }

        private Point RandomExistingCoordinate()
        {
            return _floor.Keys.ElementAt(_random.Next(0, _floor.Count));
        }

        public Area AreaAt(int X, int Y)
        {
            return AreaAt(new Point(X, Y));
        }

        public Area AreaAt(Point coordinate)
        {
            if (AreaExists(coordinate))
            {
                return _floor[coordinate]; //stucts compare value types instead of object
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"No room found at Coordinates {coordinate.X}.{coordinate.Y}, returning Limbo as special case");
                return Limbo.Instance;
            }
        }

        public bool AreaExists(int X, int Y)
        {
            return AreaExists(new Point(X, Y));
        }

        public bool AreaExists(Point coordinate)
        {
            return _floor.ContainsKey(coordinate);
        }

        public void Step()
        {

        }
    }
}