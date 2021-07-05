using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Game.Commands;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.DataStructures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Utility.Interpretation
{
    /// <summary>
    /// Responsible for analyzing user input and interpreting it with help from the Lexicon
    /// </summary>
    class Analyzer
    {
        public Lexicon Lexicon { get; private set; }

        public Analyzer()
        {
            Lexicon = new Lexicon();
        }

        /// <summary>
        /// Checks whenether a token is part of the lexicon, and returns the matching element as an argdata wrapper object
        /// </summary>
        public ArgData GetDataOf(string token)
        {
            if (Lexicon.Commands.ContainsKey(token))
            {
                Command command = (Command)Activator.CreateInstance(GetCommandTypeOf(token));
                return new ArgData(command);
            }
            else if (Lexicon.Directions.ContainsKey(token))
            {
                return new ArgData(GetDirectionOf(token));
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"Could not define key {token} as any type, returning blank data");
                return new ArgData();
            }
        }

        /// <summary>
        /// Helper function to find the command Type of a specified token
        /// Returns a special case object if a command is not found
        /// </summary>
        private Type GetCommandTypeOf(string token)
        {
            if (Lexicon.Commands.ContainsKey(token))
            {
                Logger.Instance.Info(GetType(), $"Command Type found with key: {token}, returning {Lexicon.Commands[token].Name}");
                return Lexicon.Commands[token];
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"No command found with key: {token}, returning special case object");
                return typeof(CommandNotFound);
            }
        }

        /// <summary>
        /// Helper function to find the direction Type of a specified token
        /// Returns a special case object if a direction is not found
        /// </summary>
        private Direction GetDirectionOf(string token)
        {
            if (Lexicon.Directions.ContainsKey(token))
            {
                Logger.Instance.Info(GetType(), $"Direction found with key: {token}, returning {Lexicon.Directions[token]}");
                return Lexicon.Directions[token];
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"No direction found with key: {token}, returning special case object");
                return Direction.None;
            }
        }
    }
}
