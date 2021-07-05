using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.Commands.CommandEvents;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Game.Commands
{
    internal class MoveCommand : Command
    {
        public override void Execute(KeyValuePair<string, ArgData>[] args, Subject subject)
        {
            if(args.Length > 1)
            {
                subject.Event(new MoveEvent(args[1].Value.GetDirection()));
            }
            else
            {
                subject.Event(new WriteEvent("Where to?"));
            }
        }
    }
}
