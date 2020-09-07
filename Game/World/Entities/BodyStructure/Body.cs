using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Game.World.Entities.BodyStructure
{
    internal class Body
    {
        public LinkedList<Bodypart> Bodyparts { get; private set; }

        public Body(Bodypart[] bodyparts)
        {
            Bodyparts = new LinkedList<Bodypart>(bodyparts);
        }

        public void Sever(Bodypart bodypart)
        {
            if(Bodyparts.Contains(bodypart))
            {
                Bodyparts.Remove(bodypart); //TODO: garbage collection is automatic?
            }
        }
    }
}
