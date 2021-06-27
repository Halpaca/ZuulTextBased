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
        public List<IObserver> Observers { get; private set; }

        public CommandSubject()
        {
            Observers = new List<IObserver>();
        }

        public override void Subscibe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public override void Unsubscribe(IObserver observer)
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
            foreach(IObserver observer in Observers)
            {
                observer.OnNotify(State);
            }
        }
    }
}
