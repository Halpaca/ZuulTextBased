using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands.CommandEvents;

namespace ZuulTextBased.Commands
{
    internal class MoveCommand : Command
    {
        public override void Execute(string[] args, CommandSubject subject)
        {
            if(args.Length > 1)
            {
                subject.Event(new MoveEvent());
            }
        }
    }
}
