using System;
using System.Linq;

namespace Matrix
{
    public class ResizeArray<T>
    {
        private int _capacity;
        private T[] _data;

        public int Length { get; private set; }

        public ResizeArray(T[] elems)
        {
            _data = elems;
            Length = elems.Length;
            _capacity = elems.Length;
        }
        
        public ResizeArray(int capacity = 3)
        {
            Length = 0;
            _capacity = capacity;
            _data = new T[_capacity];
        }

        private void Resize()
        {
            _capacity *= 2;
            var newData = new T[_capacity];
            for (var i = 0; i < _data.Length; i++)
            {
                newData[i] = _data[i];
            }

            _data = newData;
        }

        public T Get(int index)
        {
            if (index >= Length)
            {
                throw new IndexOutOfRangeException($"Try to get [{index}] element, but current length is {Length}");
            }

            return _data[index];
        }
        
        public void Set(int index, T value)
        {
            if (index >= Length)
            {
                throw new IndexOutOfRangeException($"Try to set [{index}] element, but current length is {Length}");
            }

            _data[index] = value;
        }
        
        public void Add(T line)
        {
            if (Length >= _capacity)
            {
                Resize();
            }

            _data[Length] = line;
            Length++;
        }

        public T[] Value()
        {
            return _data.Take(Length).ToArray();
        }
    }
}