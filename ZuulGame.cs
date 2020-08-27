using System;
using System.Net.Http.Headers;
using ZuulTextBased.Commands;
using ZuulTextBased.Commands.CommandEvents;
using ZuulTextBased.Game;
using ZuulTextBased.Utility;

namespace ZuulTextBased
{
    internal class ZuulGame : IObserver
    {
        private bool _quit = false;
        public WriteTarget WriteTarget { get; set; }
        public World World { get; private set; }
        public Player Player { get; private set; }
        public Parser Parser { get; private set; }
        public CommandFactory CommandFactory { get; private set; }
        public CommandSubject CommandSubject { get; private set; }

        public ZuulGame()
        {
            CommandSubject = new CommandSubject();
            CommandSubject.Subscibe(this);
            Player = new Player();
            CommandSubject.Subscibe(Player);
            World = new World(Player);
            Parser = new Parser();
            CommandFactory = new CommandFactory();
        }

        internal void Run()
        {
            do
            {
                string input = Console.ReadLine();
                Command c = CommandFactory.CreateCommand(Parser.GetCommand(input));
                c.Execute(Parser.Args, CommandSubject);
                World.Step();
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

        /// <summary>
        /// The only function that writes out text to the user.
        /// To be refactored to send to a custom window instead of the console
        /// </summary>
        /// <param name="message"></param>
        private void WriteOut(string message)
        {
            switch(WriteTarget)
            {
                case WriteTarget.Console:
                Console.WriteLine(message);
                    break;
            }
        }

        private void Quit()
        {
            if(PromptUser("Really Quit?", "y", "n") == true)
            {
                WriteOut("OK! See you...");
                _quit = true;
            }
            else
            {
                WriteOut("OK! Returning...");
                //TODO: write last world step
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