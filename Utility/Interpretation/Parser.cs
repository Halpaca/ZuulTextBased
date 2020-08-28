using System;
using System.Collections.Generic;
using ZuulTextBased.Commands;

namespace ZuulTextBased.Utility.Interpretation
{
    internal class Parser
    {
        public Lexicon Lexicon { get; private set; }
        public string[] Args { get; private set; }

        //Todo: make args list of key-value pairs
        //public List<KeyValuePair<string, object>> Args { get; private set; }

        public Parser()
        {
            Lexicon = new Lexicon();
        }

        public Type GetCommand(string input)
        {
            Args = input.Split(' ');
            string commandRequest = Args[0];
            return Lexicon.GetCommandTypeOf(commandRequest);
        }
    }
}