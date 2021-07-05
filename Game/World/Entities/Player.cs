
using ZuulTextBased.Game.Commands.CommandEvents;
using ZuulTextBased.Game.World.Entities;
using ZuulTextBased.Utility;
using ZuulTextBased.Utility.Logging;

namespace ZuulTextBased.Game
{
    internal class Player : Entity, IObserver
    {
        //Todo: add senses

        public Player()
        {

        }

        public override void Update()
        {
            //Empty update cycle since player is not computer controlled, can be used for confusion later maybe
        }

        public void OnNotify(Event state)
        {
            switch(state)
            {
                //TODO: Describe the rooms after a move
                case MoveEvent:
                    Logger.Instance.Info(GetType(), $"{GetType().Name} moving in direction {(Direction)state.Data}");
                    Move((Direction)state.Data);
                    break;
            }
        }

        //TODO: new write event?
    }
}