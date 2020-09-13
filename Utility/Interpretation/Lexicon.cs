using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Commands;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.DataStructures;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Utility.Interpretation
{
    class Lexicon
    {
        public Dictionary<string, Type> Commands { get; private set; }
        public Dictionary<string, Direction> Directions { get; private set; }

        public Lexicon()
        {
            StringComparer ignorecase = StringComparer.OrdinalIgnoreCase;
            Commands = new Dictionary<string, Type>(ignorecase);
            Directions = new Dictionary<string, Direction>(ignorecase);
            MapCommands();
            MapDirections();
        }

        /// <summary>
        /// Temporary function for filling the lexicon, to be migrated to an IO stream later.
        /// </summary>
        private void MapCommands()
        {
            Commands.Add("exit", typeof(QuitCommand));
            Commands.Add("quit", typeof(QuitCommand));
            Commands.Add("move", typeof(MoveCommand));
            Commands.Add("go", typeof(MoveCommand));
        }

        private void MapDirections()
        {
            Directions.Add("up", Direction.North);
            Directions.Add("north", Direction.North);
            Directions.Add("down", Direction.South);
            Directions.Add("south", Direction.South);
            Directions.Add("right", Direction.East);
            Directions.Add("east", Direction.East);
            Directions.Add("left", Direction.West);
            Directions.Add("west", Direction.West);
        }

        //TODO: Future stub for implementing IO streaming data
        public void StreamInDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
