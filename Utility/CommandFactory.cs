using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands;

namespace ZuulTextBased.Utility
{
    class CommandFactory
    {
        public Command CreateCommand(Type t)
        {
            return (Command)Activator.CreateInstance(t);
        }
    }
}
