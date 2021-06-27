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

        /// <summary>
        /// Arguments found by the interpreter. The key represents the token the user gave as input
        /// and the value the matching element found
        /// </summary>
        public KeyValuePair<string, ArgData>[] Args { get; private set; }

        public Interpreter()
        {
            Analyzer = new Analyzer();
        }

        /// <summary>
        /// Returns the value of the first argument of the argument array, which always should be a command
        /// </summary>
        public Command GetCommand()
        {
            return Args[0].Value.GetCommand();
        }

        /// <summary>
        /// Tokenizes the input string, sets the arguments and analyzes every one of them
        /// </summary>
        public void SetArguments(string userInput)
        {
            string[] tokens = userInput.Split(' ');
            Args = new KeyValuePair<string, ArgData>[tokens.Length];
            for(int i = 0; i < Args.Length; i++)
            {
                Args[i] = AnalyzeToken(tokens[i]);
            }
        }

        /// <summary>
        /// Uses the analyzer to try to find a matching element for the token
        /// </summary>
        /// <returns>A new key value pair with the oritinal token, and the found matching element</returns>
        private KeyValuePair<string, ArgData> AnalyzeToken(string token)
        {
            ArgData data = Analyzer.GetDataOf(token);
            return new KeyValuePair<string, ArgData>(token, data);
        }
    }
}