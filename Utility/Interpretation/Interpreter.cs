using System;
using System.Collections.Generic;
using ZuulTextBased.Commands;
using ZuulTextBased.Utility.DataStructures;

namespace ZuulTextBased.Utility.Interpretation
{
    /// <summary>
    /// Responsible for creating a readable argument array for Command to execute with
    /// </summary>
    internal class Interpreter
    {
        public Analyzer Analyzer { get; private set; }
        public KeyValuePair<string, ArgData>[] Args { get; private set; }

        public Interpreter()
        {
            Analyzer = new Analyzer();
        }

        public Command GetCommand()
        {
            return Args[0].Value.GetCommand();
        }

        public void SetArguments(string userInput)
        {
            string[] tokens = userInput.Split(' ');
            Args = new KeyValuePair<string, ArgData>[tokens.Length];
            for(int i = 0; i < Args.Length; i++)
            {
                Args[i] = AnalyzeToken(tokens[i]);
            }
        }

        private KeyValuePair<string, ArgData> AnalyzeToken(string token)
        {
            ArgData data = Analyzer.GetDataOf(token);
            return new KeyValuePair<string, ArgData>(token, data);
        }
    }
}