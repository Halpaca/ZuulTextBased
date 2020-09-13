
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
            switch(state)
            {
                case MoveEvent:
                    Logger.Instance.Info(GetType(), $"Entity {GetType().Name} moving in direction {((MoveEvent)state).Direction}");
                    Move(((MoveEvent)state).Direction);
                    break;
            }
        }

        //TODO: new write event?
    }
}