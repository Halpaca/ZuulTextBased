using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands;

namespace ZuulTextBased.Utility
{
    class Lexicon
    {
        public Dictionary<string, Type> Commands { get; private set; }

        public Lexicon()
        {
            Commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            Init();
        }

        public Type GetCommandOf(String s)
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
        private void Init()
        {
            Commands.Add("exit", typeof(ExitCommand));
        }
    }
}
