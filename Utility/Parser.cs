using System;
using ZuulTextBased.Commands;

namespace ZuulTextBased.Utility
{
    internal class Parser
    {
        public Lexicon Lexicon { get; private set; }

        public Parser()
        {
            Lexicon = new Lexicon();
        }

        //Todo: Tokenize input
        internal Type GetCommand(string input)
        {
            return Lexicon.GetCommandOf(input);
        }
    }
}