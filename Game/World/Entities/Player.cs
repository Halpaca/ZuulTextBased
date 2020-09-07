
using ZuulTextBased.Commands.CommandEvents;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game
{
    internal class Player : Entity, IObserver
    {
        public void OnNotify(CommandEvent state)
        {
            //TODO: move trough the rooms, and describe the rooms in the game class
            switch(state)
            {
                case MoveEvent:
                    Logger.Instance.Debug("Player", "Lets a go!");
                    break;
            }
        }
    }
}