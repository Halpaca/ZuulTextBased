
using System;
using ZuulTextBased.Commands.CommandEvents;

namespace ZuulTextBased.Commands
{
    internal class QuitCommand : Command
    {
        public override void Execute(string[] args, CommandSubject subject)
        {
            subject.Event(new QuitEvent());
        }
    }
}