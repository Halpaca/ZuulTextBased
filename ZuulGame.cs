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
        private bool _exit = false;

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
                String input = Console.ReadLine();
                Command c = CommandFactory.CreateCommand(Parser.GetCommand(input));
                c.Execute(CommandSubject);
                World.Step();
            }
            while (!_exit);
        }

        public void OnNotify(string state)
        {
            if(state.Equals("exit"))
            {
                _exit = true;
            }
        }
    }
}