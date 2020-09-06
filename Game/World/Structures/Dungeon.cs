using System;
using System.Collections.Generic;
using ZuulTextBased.Commands;

namespace ZuulTextBased.Game.World.Structures
{
    internal class Dungeon
    {
        public Room StartingRoom { get; private set; }

        public Dungeon()
        {
            StartingRoom = new Room();
        }

        public void AddToStartingRoom(Player player)
        {
            player.Enter(StartingRoom);
        }

        public void Step()
        {

        }
    }
}