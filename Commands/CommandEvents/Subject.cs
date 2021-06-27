using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Commands.CommandEvents
{
    /// <summary>
    /// Generic subject class for observer pattern
    /// </summary>
    internal abstract class Subject
    {
        public Event State { get; protected set; }

        public abstract void Subscibe(IObserver observer);

        public abstract void Unsubscribe(IObserver observer);

        public abstract void Event(Event newState);

        protected abstract void Notify();
    }
}
