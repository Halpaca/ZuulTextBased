using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands.CommandEvents;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Commands
{
    abstract class Command
    {
        public abstract void Execute(KeyValuePair<string, ArgData>[] args, Subject subject);
    }
}
