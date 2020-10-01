using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZuulTextBased.Game.World.Structures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Utility.Generation
{
    internal class BlobStrategy : FloorGenerationStrategy
    {
        public List<Point> RoomCoordinates { get; } //Represents generated room coordinates
        public List<Point> FullCoordinates { get; } //Represents room coordinates with no more valid directions to generate to

        public BlobStrategy(Floor floor) : base(floor)
        {
            RoomCoordinates = new List<Point>();
            FullCoordinates = new List<Point>();
        }

        public override void GenerateRooms(int amount)
        {
            RoomCoordinates.Add(new Point(0, 0)); //Add the floors starting room to start the generation
            while (amount > 0)
            {
                Point newCoordinate = GetRandomRoomCoordinate();
                Direction[] validDirections = GetValidDirections(newCoordinate);
                CheckNumOfDirectionsLeft(newCoordinate, validDirections.Length);
                if (validDirections.Length != 0)
                {
                    GenerateFrom(newCoordinate, RandomDirection(validDirections));
                    amount--;
                }
                //Logger.Instance.Debug(GetType(), $"{Floor.AsciiMap()}");
            }
            Logger.Instance.Debug(GetType(), $"Generation complete! showing map:\n\n" + $"{Floor.AsciiMap()}");
        }

        /// <summary>
        /// Returns a random coordinate that represents a room, exclusing coordinates that are full
        /// </summary>
        private Point GetRandomRoomCoordinate()
        {
            return RoomCoordinates.ElementAt(Random.Next(0, RoomCoordinates.Count));
        }

        /// <summary>
        /// Returns an array of all directions that don't point to 
        /// existsing room coordinates or existing full room coordinates
        /// </summary>
        private Direction[] GetValidDirections(Point source)
        {
            List<Direction> directions = new List<Direction>();
            foreach (Direction direction in Directions.All)
            {
                if (PointInDirectionIsValid(source, direction))
                {
                    directions.Add(direction);
                }
            }
            return directions.ToArray();
        }

        /// <summary>
        /// Creates a point relative to the source and checks if it doesn't overlap
        /// existsing room coordinates or existing full room coordinates
        /// </summary>
        private bool PointInDirectionIsValid(Point source, Direction direction)
        {
            Point offset = Points.Add(source, Directions.ToPoint(direction));
            return !RoomCoordinates.Contains(offset) && !FullCoordinates.Contains(offset);
        }

        /// <summary>
        /// Checks if the number of valid directions left of the source coordinate is 1 or less.
        /// If so, the coordinate has (or is going to have) no more valid directions left, and is moved to full room coordinates
        /// </summary>
        private void CheckNumOfDirectionsLeft(Point source, int NumValidDirections)
        {
            if (NumValidDirections <= 1) //<= 1 because this last valid direction will be used up, going down to 0
            {
                Logger.Instance.Debug(GetType(), $"Point {source.X}.{source.Y} has no more viable directions left, moving it to Full Members");
                RoomCoordinates.Remove(source);
                FullCoordinates.Add(source);
            }
        }

        /// <summary>
        /// Generates a room from the source coordinate to the direction specified, then links those rooms together with an entrance
        /// Finally, the new coordinate is added to the room coordinates to be picked for future passes of the algorithm
        /// </summary>
        private void GenerateFrom(Point source, Direction direction)
        {
            Point nextRoom = Points.Add(source, Directions.ToPoint(direction));
            Floor.CreateRoom(nextRoom);
            Floor.LinkAreas(source, direction, nextRoom);
            RoomCoordinates.Add(nextRoom);
        }

        /// <summary>
        /// Picks a random direction from the array specified, which are all found valid directions
        /// </summary>
        private Direction RandomDirection(Direction[] directions)
        {
            return directions[Random.Next(0, directions.Length)];
        }
    }
}