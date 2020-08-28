using System;
using System.Collections.Generic;
using ZuulTextBased.Commands;

namespace ZuulTextBased.Game.World
{
    internal class Dungeon
    {
        public LinkedList<Player> Players { get; private set; }

        public Dungeon()
        {
            Players = new LinkedList<Player>();
        }

        public void Enter(Player player)
        {
            Players.AddLast(player);
        }

        public void Leave(Player player)
        {
            if(Players.Contains(player))
            {
                Players.Remove(player);
            }
        }

        public void Step()
        {

        }
    }
}