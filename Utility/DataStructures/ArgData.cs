﻿using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Utility.DataStructures
{
    /// <summary>
    /// Wrapper Class used to contain data red out by Lexicon.
    /// Used by Parser to make argument lists.
    /// </summary>
    class ArgData
    {
        public ArgType Type { get; private set; }

        public object Container { get; private set; }

        public ArgData()
        {
            Type = ArgType.Undefined;
            Container = new NullArgument();
        }

        public ArgData(Command Command)
        {
            Type = ArgType.Command;
            Container = Command;
        }

        public ArgData(Direction direction)
        {
            Type = ArgType.Direction;
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
                Logger.Instance.Warn(GetType(), $"This data's container is not a type of command, but {Container.GetType().Name}. Returning special case object");
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
                Logger.Instance.Warn(GetType(), $"This data's container is not a type of direction, but {Container.GetType().Name}. Returning special case object");
                return Direction.None;
            }
        }
    }
}
