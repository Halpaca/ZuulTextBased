using System;
using System.Collections.Generic;
using System.Text;
using ZuulTextBased.Utility;

namespace ZuulTextBased.Commands.CommandEvents
{
    class CommandSubject
    {
        public List<IObserver> Observers { get; private set; }
        public string State { get; private set; }

        public CommandSubject()
        {
            State = String.Empty;
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
        public void SetState(string newState)
        {
            if (!State.Equals(newState))
            {
                State = newState;
                NotifyObservers();
            }
        }

        private void NotifyObservers()
        {
            foreach(IObserver observer in Observers)
            {
                observer.OnNotify(State);
            }
        }
    }
}
