using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.Commands.CommandEvents;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Game.Commands
{
    abstract class Command
    {
        public abstract void Execute(KeyValuePair<string, ArgData>[] args, Subject subject);
    }
}
