using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands.CommandEvents;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Commands
{
    internal class MoveCommand : Command
    {
        //TODO: exept up/down from here, not move event
        public override void Execute(KeyValuePair<string, ArgData>[] args, CommandSubject subject)
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
