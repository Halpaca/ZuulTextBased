using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZuulTextBased.Utility
{
    /// <summary>
    /// Utility class for Direction, which adds some helper functions and consts
    /// </summary>
    internal static class Directions
    {
        public readonly static int Count = Enum.GetValues(typeof(Direction)).Length - 1; //-1 for Direction.None
        public readonly static Direction[] All = GetAllDirections();

        public static Direction Invert(Direction direction)
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

        private static Direction[] GetAllDirections()
        {
            return Enum.GetValues(typeof(Direction))
                       .Cast<Direction>()
                       .Except(new Direction[] { Direction.None })
                       .ToArray();
        }
    }
}
