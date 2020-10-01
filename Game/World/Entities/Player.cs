
using ZuulTextBased.Commands.CommandEvents;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game
{
    internal class Player : Entity, ICommandObserver
    {
        //Todo: add senses

        public Player()
        {

        }

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

        public override void Update()
        {
            //Empty update cycle since player is not computer controlled, can be used for confusion later maybe
        }

        //TODO: new write event?
    }
}