using System;
using System.Collections.Generic;

namespace MineIT.CityBuilder.Simulation.Scheduling
{
    internal sealed class StableMinHeap<T> where T : IComparable<T>
    {
        private readonly List<T> _items = new List<T>();

        public int Count => _items.Count;

        public void Clear() => _items.Clear();

        public void Push(T item)
        {
            _items.Add(item);
            var index = _items.Count - 1;

            while (index > 0)
            {
                var parent = (index - 1) / 2;
                if (_items[parent].CompareTo(_items[index]) <= 0)
                {
                    break;
                }

                Swap(parent, index);
                index = parent;
            }
        }

        public bool TryPeek(out T item)
        {
            if (_items.Count == 0)
            {
                item = default;
                return false;
            }

            item = _items[0];
            return true;
        }

        public bool TryPop(out T item)
        {
            if (_items.Count == 0)
            {
                item = default;
                return false;
            }

            item = _items[0];
            var last = _items.Count - 1;
            _items[0] = _items[last];
            _items.RemoveAt(last);

            var index = 0;
            while (true)
            {
                var left = index * 2 + 1;
                if (left >= _items.Count)
                {
                    break;
                }

                var right = left + 1;
                var smallest = right < _items.Count && _items[right].CompareTo(_items[left]) < 0
                    ? right
                    : left;

                if (_items[index].CompareTo(_items[smallest]) <= 0)
                {
                    break;
                }

                Swap(index, smallest);
                index = smallest;
            }

            return true;
        }

        public T[] ExportSorted()
        {
            var copy = _items.ToArray();
            Array.Sort(copy);
            return copy;
        }

        private void Swap(int left, int right)
        {
            var value = _items[left];
            _items[left] = _items[right];
            _items[right] = value;
        }
    }
}
