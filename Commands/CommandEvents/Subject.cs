using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Commands.CommandEvents
{
    internal abstract class Subject
    {
        public Event State { get; protected set; }

        public abstract void Subscibe(ICommandObserver observer);

        public abstract void Unsubscribe(ICommandObserver observer);

        public abstract void Event(Event newState);

        protected abstract void Notify();
    }
}
