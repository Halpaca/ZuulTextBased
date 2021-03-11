using System;
using System.Net.Http.Headers;
using ZuulTextBased.Commands;
using ZuulTextBased.Commands.CommandEvents;
using ZuulTextBased.Game;
using ZuulTextBased.Game.World;
using ZuulTextBased.Game.World.Structures;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Interpretation;

namespace ZuulTextBased
{
    internal class ZuulGame : ICommandObserver
    {
        private bool _quit = false;
        public WriteMode WriteTarget { get; set; }
        public Dungeon Dungeon { get; private set; }
        public Player Player { get; private set; }
        public Parser Parser { get; private set; }
        public CommandSubject CommandSubject { get; private set; }

        public ZuulGame()
        {
            CommandSubject = new CommandSubject();
            CommandSubject.Subscibe(this);

            Player = new Player();
            CommandSubject.Subscibe(Player);

            Dungeon = new Dungeon();
            Dungeon.GenerateActiveFloor(20);
            Dungeon.AddToStartingFloor(Player);

            Parser = new Parser();
        }

        //TODO: Describe the rooms in the game class
        internal void Run()
        {
            WriteOut("Welcome, type stuff below:");
            do
            {
                Console.Write("> ");
                AwaitUserInput();
                ExecuteNextCommand();
                Dungeon.Update();
            }
            while (!_quit);
        }

        public void OnNotify(CommandEvent state)
        {
            switch(state)
            {
                case WriteEvent:
                    WriteOut(((WriteEvent)state).Message);
                    break;
                case QuitEvent:
                    Quit();
                    break;
            }
        }

        private void AwaitUserInput()
        {
            string input = Console.ReadLine();
            Parser.SetArguments(input);
        }

        private void ExecuteNextCommand()
        {
            Command c = Parser.GetCommand();
            c.Execute(Parser.Args, CommandSubject);
        }

        /// <summary>
        /// The only function that should write out text to the player.
        /// To be refactored to send to a custom window instead of the console
        /// </summary>
        /// <param name="message">The message to be printed</param>
        private void WriteOut(string message)
        {
            switch(WriteTarget)
            {
                case WriteMode.Console:
                Console.WriteLine(message);
                    break;
            }
        }

        private void Quit()
        {
            if(PromptUser("Really Quit?", "y", "n") == true)
            {
                WriteOut("OK, see you!");
                _quit = true;
            }
            else
            {
                WriteOut("OK! Returning...");
                //TODO: write last world step as reminder
            }
        }

        private bool PromptUser(string question, string positive, string negative)
        {
            WriteOut(question + $" {positive}/{negative}");
            string input = Console.ReadLine();
            if(input.Equals(positive, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if(input.Equals(negative, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                WriteOut($"Please answer with {positive} or {negative}");
                return PromptUser(question, positive, negative);
            }
        }

        ~ZuulGame()
        {
            CommandSubject.Unsubscribe(this);
        }
    }
}