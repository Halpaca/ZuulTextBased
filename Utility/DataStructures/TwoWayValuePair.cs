using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Utility.DataStructures
{
    public class TwoWayValuePair<T>
    {
        public T Key { get; private set; }
        public T Value { get; private set;  }

        public TwoWayValuePair(T first, T second)
        {
            Key = first;
            Value = second;
        }

        public void SetValues(T first, T second)
        {
            Key = first;
            Value = second;
        }

        public T this[T valueIn]
        {
            get
            {
                if (Key.Equals(valueIn))
                {
                    return Value;
                }
                if (Value.Equals(valueIn))
                {
                    return Key;
                }
                throw new ArgumentException(nameof(valueIn));
            }
        }
    }
}
