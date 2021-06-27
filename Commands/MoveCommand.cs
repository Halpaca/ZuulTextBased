using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands.CommandEvents;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Commands
{
    internal class MoveCommand : Command
    {
        public override void Execute(KeyValuePair<string, ArgData>[] args, CommandSubject commandSubject)
        {
            if(args.Length > 1)
            {
                commandSubject.Event(new MoveEvent(args[1].Value.GetDirection()));
            }
            else
            {
                commandSubject.Event(new WriteEvent("Where to?"));
            }
        }
    }
}
