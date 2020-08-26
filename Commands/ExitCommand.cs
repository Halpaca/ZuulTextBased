
using System;
using ZuulTextBased.Commands.CommandEvents;

namespace ZuulTextBased.Commands
{
    internal class ExitCommand : Command
    {
        public override void Execute(CommandSubject subject)
        {
            subject.SetState("exit");
        }
    }
}