using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands.CommandEvents;

namespace ZuulTextBased.Commands
{
    abstract class Command
    {
        public abstract void Execute(string[] args, CommandSubject subject);
    }
}
