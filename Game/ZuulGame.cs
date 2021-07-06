using System;
using System.Drawing;
using System.Text;
using ZuulTextBased.Game.Commands;
using ZuulTextBased.Game.Commands.CommandEvents;
using ZuulTextBased.Game.View;
using ZuulTextBased.Game.World.Structures;
using ZuulTextBased.Utility.Interpretation;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game
{
    /// <summary>
    /// Main class, handles the game loop
    /// </summary>
    internal class ZuulGame : IObserver
    {
        public bool Quit { get; private set; }
        public Dungeon Dungeon { get; private set; }
        public Interpreter Interpreter { get; private set; }
        public Writer Writer { get; private set; }
        public CommandSubject CommandSubject { get; private set; }

        public ZuulGame()
        {
            CommandSubject = new CommandSubject();
            CommandSubject.Subscibe(this);

            Dungeon = new Dungeon();
            Dungeon.GenerateActiveFloor(20);

            Player player = new Player();
            CommandSubject.Subscibe(player);
            player.MoveToArea(Dungeon.GetActiveFloor().AreaAt(new Point(0, 0)));

            Interpreter = new Interpreter();

            Writer = new Writer(new Screen("GameScreen", 0, 0, 127, 47));
            Writer.ActiveScreen.AddSubScreen(new Screen("Test1", 0, 0, 127, 43));
            //TODO: Fix the Non overlap
            Writer.ActiveScreen.AddSubScreen(new Screen("Test2", 0, 43, 127, 4));
            CommandSubject.Subscibe(Writer);
        }

        /// <summary>
        /// Event handling for the write and quit events.
        /// Used when quitting the game or writing to the frontend.
        /// </summary>
        public void OnNotify(Event state)
        {
            switch (state)
            {
                case QuitEvent:
                    QuitGame();
                    break;
            }
        }

        /// <summary>
        /// Game Loop, does a step for the player, then the dungeon
        /// </summary>
        internal void Run()
        {
            Writer.Write("Welcome, type stuff below:");
            do
            {
                Draw();
                PlayerStep();
                DungeonStep();
            }
            while (!Quit);
        }

        private void PlayerStep()
        {
            AwaitUserInput();
            ExecuteNextCommand();
        }

        private void DungeonStep()
        {
            Dungeon.Update();
        }

        /// <summary>
        /// Reads input of the user, 
        /// then sends the string off to the interpreter for tokenization and analyzation
        /// </summary>
        private void AwaitUserInput()
        {
            string input = Console.ReadLine();
            Interpreter.SetArguments(input);
        }

        /// <summary>
        /// Executes the command found by the interpreter, and adds any other arguments given by the player
        /// </summary>
        private void ExecuteNextCommand()
        {
            Command c = Interpreter.GetCommand();
            c.Execute(Interpreter.Args, CommandSubject);
        }

        /// <summary>
        /// Promts the user to quit and breaks the game loop on a yes.
        /// On a no the game returns to the loop
        /// </summary>
        private void QuitGame()
        {
            if(PromptUser("Really Quit?", "y", "n") == true)
            {
                Writer.Write("OK, see you!");
                Quit = true;
            }
            else
            {
                //TODO: returning should skip the dungeon update step, it should still be the players turn
                Writer.Write("OK! Returning...");
                //TODO: write last world update step as reminder of what happened
            }
        }

        /// <summary>
        /// Promts the user, then returns true or false based on the answer
        /// </summary>
        private bool PromptUser(string question, string y, string n)
        {
            Writer.Write(question + $" {y}/{n}");
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
                Writer.Write($"Please answer with {y} or {n}");
                return PromptUser(question, y, n);
            }
        }

        /// <summary>
        /// Experimental
        /// Test Function for future implementation for the Writer class and to be changed later
        /// Not fully implemented, currently used to draw the main screen and try custom input
        /// </summary>
        private void Draw()
        {
            ConsoleKeyInfo KeyInfo;
            StringBuilder userInput = new StringBuilder();

            Writer.Draw();
            do
            {
                KeyInfo = Console.ReadKey();
                //TODO: KeyPressEvent for writer to swap buffers
                userInput.Append(KeyInfo.KeyChar);
            }
            while (KeyInfo.Key != ConsoleKey.Enter);
            Logger.Instance.Debug(GetType(), $"New user input string: {userInput}");
        }

        ~ZuulGame()
        {
            CommandSubject.Unsubscribe(this);
        }
    }
}