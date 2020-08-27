using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Utility;

namespace ZuulTextBased.Commands.CommandEvents
{
    class CommandSubject
    {
        public List<IObserver> Observers { get; private set; }
        public CommandEvent State { get; private set; }

        public CommandSubject()
        {
            Observers = new List<IObserver>();
        }

        public void Subscibe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            if(Observers.Contains(observer))
            {
                Observers.Remove(observer);
            }
        }
        public void Event(CommandEvent newState)
        {
            State = newState;
            Notify();
        }

        private void Notify()
        {
            foreach(IObserver observer in Observers)
            {
                observer.OnNotify(State);
            }
        }
    }
}
