    using System;
using System.Collections.Generic;
using ZuulTextBased.Commands;
using ZuulTextBased.Utility;

namespace ZuulTextBased.Game.World.Structures
{
    internal class Dungeon
    {
        public Room StartingRoom { get; private set; }

        public Dungeon()
        {
            StartingRoom = new Room(0, 0);
            //TODO: Test code for moving between rooms, to be moved to room and entrance to make bi-directional links
            Room room1 = new Room(1, 0);
            Door door = new Door(StartingRoom, room1);
            StartingRoom.AddExit(door, Direction.North);
            room1.AddExit(door, Direction.South);
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