using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Game.World.Entities.BodyStructure
{
    internal class Body
    {
        public LinkedList<Bodypart> Bodyparts { get; private set; }

        public Body()
        {
            Bodyparts = new LinkedList<Bodypart>();
        }

        public Body(Bodypart[] bodyparts)
        {
            Bodyparts = new LinkedList<Bodypart>(bodyparts);
        }

        public void Sever(Bodypart bodypart)
        {
            if(Bodyparts.Contains(bodypart))
            {
                Bodyparts.Remove(bodypart);
            }
        }

        //TODO: support more modes of transport (flying, slithering, swimming)
        public bool CanMove()
        {
            foreach(Bodypart bodypart in Bodyparts)
            {
                if(bodypart.Type == BodypartType.Leg)
                {
                    return true;
                }
            }
            return false;
        }

        public void CanWalk()
        {

        }
    }
}
