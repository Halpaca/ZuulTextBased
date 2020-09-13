using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZuulTextBased.Game.World.Structures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Utility.Generation
{
    internal class BlobGenerationStrategy : FloorGenerationStrategy
    {
        public List<Point> GeneratedRooms { get; } //Represents generated room coordinates
        public List<Point> FullRooms { get; } //Represents room coordinates with no more valid directions to generate to

        public BlobGenerationStrategy(Floor floor) : base(floor)
        {
            GeneratedRooms = new List<Point>();
            FullRooms = new List<Point>();
        }

        public override void GenerateRooms(int amount)
        {
            GeneratedRooms.Add(new Point(0, 0)); //Add the floors starting room to start the generation
            while (amount > 0)
            {
                Point currentPoint = GetRandomValidPoint();
                Direction[] validDirections = GetValidDirections(currentPoint);
                CheckNumOfDirectionsLeft(currentPoint, validDirections.Length);
                if (validDirections.Length != 0)
                {
                    GenerateFrom(currentPoint, RandomDirection(validDirections));
                    amount--;
                }
            }
        }

        private Point GetRandomValidPoint()
        {
            return GeneratedRooms.ElementAt(Random.Next(0, GeneratedRooms.Count));
        }

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

        private bool PointInDirectionIsValid(Point source, Direction direction)
        {
            Point offset = Points.Add(source, Directions.ToPoint(direction));
            return !GeneratedRooms.Contains(offset) && !FullRooms.Contains(offset);
        }

        private void CheckNumOfDirectionsLeft(Point point, int NumValidDirections)
        {
            if (NumValidDirections <= 1) //<= 1 because this last valid direction will be used up, going down to 0
            {
                Logger.Instance.Debug(GetType(), $"Point {point.X}.{point.Y} has no more viable directions left, moving it to Full Members");
                GeneratedRooms.Remove(point);
                FullRooms.Add(point);
            }
        }

        private void GenerateFrom(Point current, Direction direction)
        {
            Point nextRoom = Points.Add(current, Directions.ToPoint(direction));
            Floor.CreateRoom(nextRoom);
            Floor.LinkAreas(current, direction, nextRoom);
            GeneratedRooms.Add(nextRoom);
        }

        private Direction RandomDirection(Direction[] directions)
        {
            return directions[Random.Next(0, directions.Length)];
        }
    }
}
