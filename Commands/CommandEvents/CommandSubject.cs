using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Utility;

namespace ZuulTextBased.Commands.CommandEvents
{
    /// <summary>
    /// Subject class using observer pattern. Used to send Events to listeners.
    /// </summary>
    internal class CommandSubject : Subject
    {
        public List<ICommandObserver> Observers { get; private set; }

        public CommandSubject()
        {
            Observers = new List<ICommandObserver>();
        }

        public override void Subscibe(ICommandObserver observer)
        {
            Observers.Add(observer);
        }

        public override void Unsubscribe(ICommandObserver observer)
        {
            if(Observers.Contains(observer))
            {
                Observers.Remove(observer);
            }
        }

        public override void Event(Event newState)
        {
            State = newState;
            Notify();
        }

        protected override void Notify()
        {
            foreach(ICommandObserver observer in Observers)
            {
                observer.OnNotify(State);
            }
        }
    }
}
