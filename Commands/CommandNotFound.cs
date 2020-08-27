﻿using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands.CommandEvents;

namespace ZuulTextBased.Commands
{
    /// <summary>
    /// Special Case Object for returning invalid commands
    /// </summary>
    class CommandNotFound : Command
    {
        public override void Execute(string[] args, CommandSubject subject)
        {
            subject.Event(new WriteEvent($"Command not found with name: {args[0]}"));
        }
    }
}
