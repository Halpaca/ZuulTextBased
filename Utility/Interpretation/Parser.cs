using System;
using System.Collections.Generic;
using ZuulTextBased.Commands;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Utility.Interpretation
{
    /// <summary>
    /// Responsible for creating a readable argument array for Command to execute with
    /// </summary>
    internal class Parser
    {
        public Analyzer Analyzer { get; private set; }

        //Todo: make args list of key-value pairs
        public KeyValuePair<string, ArgData>[] Args { get; private set; }

        public Parser()
        {
            Analyzer = new Analyzer();
        }

        public Command GetCommand()
        {
            return Args[0].Value.GetCommand();
        }

        public KeyValuePair<string, ArgData>[] Analyze(string source)
        {
            string[] tokens = source.Split(' ');
            Args = new KeyValuePair<string, ArgData>[tokens.Length];
            for(int i = 0; i < Args.Length; i++)
            {
                Args[i] = AnalyzeToken(tokens[i]);
            }
            return Args;
        }

        private KeyValuePair<string, ArgData> AnalyzeToken(string token)
        {
            ArgData data = Analyzer.GetDataOf(token);
            return new KeyValuePair<string, ArgData>(token, data);
        }
    }
}