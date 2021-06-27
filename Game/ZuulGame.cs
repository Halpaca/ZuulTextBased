using System;
using ZuulTextBased.Commands;
using ZuulTextBased.Commands.CommandEvents;
using ZuulTextBased.Game.World.Structures;
using ZuulTextBased.Utility.Interpretation;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game
{
    /// <summary>
    /// Main class, handles the game loop
    /// </summary>
    internal class ZuulGame : ICommandObserver
    {
        public bool Quit { get; private set; }
        public WriteMode WriteTarget { get; set; } //TODO: move responsibility of writing to a writer class(?)
        public Dungeon Dungeon { get; private set; }
        public Interpreter Interpreter { get; private set; }
        public CommandSubject CommandSubject { get; private set; }

        public ZuulGame()
        {
            CommandSubject = new CommandSubject();
            CommandSubject.Subscibe(this);

            Player player = new Player();
            CommandSubject.Subscibe(player);

            Dungeon = new Dungeon();
            Dungeon.GenerateActiveFloor(20);
            Dungeon.AddToStartingFloor(player);

            Interpreter = new Interpreter();
        }

        /// <summary>
        /// Game Loop, does a step for the player, then the dungeon
        /// </summary>
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
            while (!Quit);
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnNotify(Event state)
        {
            switch(state)
            {
                case WriteEvent:
                    WriteOut(((WriteEvent)state).Message);
                    break;
                case QuitEvent:
                    QuitGame();
                    break;
            }
        }

        private void AwaitUserInput()
        {
            string input = Console.ReadLine();
            Interpreter.SetArguments(input);
        }

        private void ExecuteNextCommand()
        {
            Command c = Interpreter.GetCommand();
            c.Execute(Interpreter.Args, CommandSubject);
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

        private void QuitGame()
        {
            if(PromptUser("Really Quit?", "y", "n") == true)
            {
                WriteOut("OK, see you!");
                Quit = true;
            }
            else
            {
                WriteOut("OK! Returning...");
                //TODO: write last world step as reminder
            }
        }

        private bool PromptUser(string question, string y, string n)
        {
            WriteOut(question + $" {y}/{n}");
            string input = Console.ReadLine();
            if(input.Equals(y, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if(input.Equals(n, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                WriteOut($"Please answer with {y} or {n}");
                return PromptUser(question, y, n);
            }
        }

        ~ZuulGame()
        {
            CommandSubject.Unsubscribe(this);
        }
    }
}