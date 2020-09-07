using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Utility.DataStructures
{
    public class TwoWayValuePair<T>
    {
        private readonly KeyValuePair<T, T> _twoWayPair;
        public T Key { get; private set; }
        public T Value { get; private set; }

        public TwoWayValuePair(T first, T second)
        {
            _twoWayPair = new KeyValuePair<T, T>(first, second);
            Key = first;
            Value = second;
        }

        public T this[T value]
        {
            get
            {
                if (_twoWayPair.Key.Equals(value))
                {
                    return _twoWayPair.Value;
                }
                if (_twoWayPair.Value.Equals(value))
                {
                    return _twoWayPair.Key;
                }
                throw new ArgumentException(nameof(value));
            }
        }
    }
}
