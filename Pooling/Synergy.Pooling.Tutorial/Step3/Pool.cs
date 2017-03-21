using System;
using System.Collections.Generic;

namespace Synergy.Pooling.Tutorial.Step3
{
    public class Pool<T>
    {
        private readonly Func<T> constructor;
        private readonly Stack<T> _items = new Stack<T>();
        private readonly object _sync = new object();

        public Pool(Func<T> constructor)
        {
            this.constructor = constructor;
        }

        public T Get()
        {
            lock (this._sync)
            {
                if (this._items.Count == 0)
                    return this.constructor();

                return this._items.Pop();
            }
        }

        public void Free(T item)
        {
            lock (this._sync)
            {
                this._items.Push(item);
            }
        }
    }
}