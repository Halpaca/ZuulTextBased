using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.Commands;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Utility.DataStructures
{
    /// <summary>
    /// Wrapper Class used to contain data red out by Lexicon.
    /// Used by Parser to make argument lists.
    /// </summary>
    internal class ArgData
    {
        public ArgType ArgType { get; private set; }

        public object Container { get; private set; }

        public ArgData()
        {
            ArgType = ArgType.Undefined;
            Container = new NullArgument();
        }

        public ArgData(Command Command)
        {
            ArgType = ArgType.Command;
            Container = Command;
        }

        public ArgData(Direction direction)
        {
            ArgType = ArgType.Direction;
            Container = direction;
        }

        public Command GetCommand()
        {
            if(Container is Command command)
            {
                return command;
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"This data's container is not a type of command, but {Container.GetType().Name}. Returning CommandNotFound");
                return new CommandNotFound();
            }
        }

        public Direction GetDirection()
        {
            if (Container is Direction direction)
            {
                return direction;
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"This data's container is not a type of direction, but {Container.GetType().Name}. Returning Direction.None");
                return Direction.None;
            }
        }
    }
}
