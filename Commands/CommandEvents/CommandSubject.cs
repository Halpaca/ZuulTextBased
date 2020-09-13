using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Utility;

namespace ZuulTextBased.Commands.CommandEvents
{
    class CommandSubject
    {
        public List<ICommandObserver> Observers { get; private set; }
        public CommandEvent State { get; private set; }

        public CommandSubject()
        {
            Observers = new List<ICommandObserver>();
        }

        public void Subscibe(ICommandObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(ICommandObserver observer)
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
            foreach(ICommandObserver observer in Observers)
            {
                observer.OnNotify(State);
            }
        }
    }
}
