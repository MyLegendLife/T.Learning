using System;

namespace T.Learning.Arithmetic
{
    public interface IListEx<T>
    {
        int Count { get; }

        T this[int index] { get; set; }

        void Add(T t);

        void Remove(T t);

        void RemoveAt(int index);
    }

    public class ListEx<T> : IListEx<T>
    {
        private int _size;
        private T[] _items;

        public int Count => _size;

        private void EnsureCapacity(int min)
        {
            if (_items.Length > min)
            {
                return;
            }

            var num = _items.Length == 0 ? 4 : _items.Length * 2;
            if (num > 2000000000U)
            {
                num = 2000000000;
            }

            Capacity = num;
        }

        public int Capacity
        {
            get => _items.Length;
            set
            {
                var newItems = new T[value];

                Array.Copy(_items,newItems,_size);

                _items = newItems;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _items[index];
            }
            set
            {
                if (index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _items[index] = value;
            }
        }

        public void Add(T item)
        {
            _items[_size] = item;
            _size++;
        }

        public void Remove(T t)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}
