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
            return direction switch
            {
                Direction.North => Direction.South,
                Direction.East => Direction.West,
                Direction.South => Direction.North,
                Direction.West => Direction.East,
                _ => Direction.None
            };
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
            return direction switch
            {
                Direction.North => new Point(0, -1),
                Direction.East => new Point(1, 0),
                Direction.South => new Point(0, 1),
                Direction.West => new Point(-1, 0),
                _ => new Point(0, 0)
            };
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
