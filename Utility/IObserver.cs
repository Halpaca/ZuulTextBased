using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Utility
{
    interface IObserver
    {
        public abstract void OnNotify(string state);
    }
}
