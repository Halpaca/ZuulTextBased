using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.Commands.CommandEvents;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Game.Commands
{
    /// <summary>
    /// Special Case Object for returning invalid commands
    /// </summary>
    class CommandNotFound : Command
    {
        public override void Execute(KeyValuePair<string, ArgData>[] args, Subject subject)
        {
            subject.Event(new WriteEvent($"Command not found with name: {args[0].Key}"));
        }
    }
}
