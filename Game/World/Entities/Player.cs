﻿
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
                    Logger.Instance.Info(GetType(), $"Entity {GetType().Name} moving in direction {(Direction)state.data}");
                    Move((Direction)state.data);
                    break;
            }
        }

        //TODO: new write event?
    }
}