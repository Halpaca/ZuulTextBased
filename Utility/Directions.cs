using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ZuulTextBased.Utility
{
    /// <summary>
    /// Utility class for Direction, which adds some helper functions and constants
    /// </summary>
    internal static class Directions
    {
        public readonly static int Count = Enum.GetValues(typeof(Direction)).Length - 1; //-1 for Direction.None
        public readonly static Direction[] All = GetAllDirections();
        private readonly static Random _random = new Random();

        public static Direction Inverse(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                case Direction.None:
                default:
                    return Direction.None;
            }
        }

        /// <summary>
        /// Returns a random direction, excluding Direction.None
        /// </summary>
        public static Direction RandomValid()
        {
            return All[_random.Next(0, All.Length)];
        }

        public static Point ToPoint(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Point(0, -1);
                case Direction.East:
                    return new Point(1, 0);
                case Direction.South:
                    return new Point(0, 1);
                case Direction.West:
                    return new Point(-1, 0);
                case Direction.None:
                default:
                    return new Point(0, 0);
            }
        }

        private static Direction[] GetAllDirections()
        {
            return Enum.GetValues(typeof(Direction))
                       .Cast<Direction>()
                       .Except(new Direction[] { Direction.None })
                       .ToArray();
        }
    }
}
