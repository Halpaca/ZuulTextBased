
using System;
using System.Collections.Generic;
using ZuulTextBased.Game.Commands.CommandEvents;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Game.Commands
{
    internal class QuitCommand : Command
    {
        public override void Execute(KeyValuePair<string, ArgData>[] args, Subject subject)
        {
            subject.Event(new QuitEvent());
        }
    }
}