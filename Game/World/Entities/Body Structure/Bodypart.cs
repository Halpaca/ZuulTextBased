﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Game.World.Entities.Body_Structure
{
    internal class Bodypart
    {
        public BodypartType Type { get; private set; }
        //Used to keep track of this body parts statuses and their severities
        private LinkedList<Status> _statuses;

        public bool IsVital { get; set; }

        public Bodypart(BodypartType type)
        {
            Type = type;
            _statuses = new LinkedList<Status>();
        }

        public void AddStatus(Status status)
        {
            _statuses.AddLast(status);
        }
    }
}
