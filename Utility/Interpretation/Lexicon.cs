using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands;

namespace ZuulTextBased.Utility.Interpretation
{
    class Lexicon
    {
        public Dictionary<string, Type> Commands { get; private set; }

        public Lexicon()
        {
            Commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            MapCommands();
        }

        public Type GetCommandTypeOf(String s)
        {
            if (Commands.ContainsKey(s))
            {
                return Commands[s];
            }
            else
            {
                return typeof(CommandNotFound);
            }
        }

        /// <summary>
        /// Temporary function for filling the lexicon, to be migrated to an IO stream later.
        /// </summary>
        private void MapCommands()
        {
            Commands.Add("exit", typeof(QuitCommand));
            Commands.Add("quit", typeof(QuitCommand));
        }
    }
}
