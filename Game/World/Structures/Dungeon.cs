using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ZuulTextBased.Commands;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game.World.Structures
{
    /// <summary>
    /// Provides a basic dungeon structure for entities to traverse in
    /// Default generates with a starting floor at Index 0
    /// </summary>
    internal class Dungeon
    {
        public List<Floor> Floors { get; private set; }
        private int _activeFloor;

        public Dungeon()
        {
            Floors = new List<Floor>();
            AddFloor(0);
            _activeFloor = 0;
        }

        public void Update()
        {
            Floors[_activeFloor].Update();
        }

        /// <summary>
        /// Function that is called once at the start of the game, 
        /// after the player has subscribed to the Command Subject
        /// </summary>
        public void AddToStartingFloor(Player player)
        {
            Floors[0].EnterPlayer(player);
        }

        public void AddFloor(int floorNumber)
        {
            if(Floors.ElementAtOrDefault(floorNumber) == null)
            {
                Floors.Add(new Floor());
            }
            else
            {
                Logger.Instance.Warn(GetType(), $"Floor already exists at index {floorNumber}");
            }
        }

        public void GenerateActiveFloor(int areaCount)
        {
            Floors[_activeFloor].GenerateRooms(areaCount);
        }
    }
}