using System;
using System.Collections.Generic;
using System.Threading;

namespace MunicipalityApplicatiion.DataStructures
{
    public class PriorityQueue<T>
    {
        // List to store items with their priority and insertion sequence
        private readonly List<(T item, int priority, long seq)> _data = new();
        private static long _seqGen = 0;

        // Returns the number of items in the queue.
        public int Count => _data.Count;

        // Inserts an item with a given priority.
        public void Insert(T item, int priority)
        {
            var seq = Interlocked.Increment(ref _seqGen);
            _data.Add((item, priority, seq));
            HeapifyUp(_data.Count - 1);
        }

        // Removes and returns the highest-priority item.
        public T ExtractMax()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("Priority queue is empty.");

            var ret = _data[0].item;
            _data[0] = _data[^1];
            _data.RemoveAt(_data.Count - 1);
            if (_data.Count > 0)
                HeapifyDown(0);

            return ret;
        }

        // Returns the highest-priority item without removing it.
        public T Peek()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("Priority queue is empty.");
            return _data[0].item;
        }

        // Removes all items from the queue.
        public void Clear() => _data.Clear();

        // Internal helpers
        private static bool Greater((T item, int priority, long seq) a,
                                    (T item, int priority, long seq) b)
        {
            // Higher priority first
            if (a.priority != b.priority)
                return a.priority > b.priority;
            return a.seq < b.seq;
        }

        // Heapify methods
        private void HeapifyUp(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (!Greater(_data[i], _data[p])) break;
                (_data[i], _data[p]) = (_data[p], _data[i]);
                i = p;
            }
        }

        // Heapify down from index i
        private void HeapifyDown(int i)
        {
            while (true)
            {
                int l = 2 * i + 1, r = l + 1, best = i;
                if (l < _data.Count && Greater(_data[l], _data[best])) best = l;
                if (r < _data.Count && Greater(_data[r], _data[best])) best = r;
                if (best == i) break;
                (_data[i], _data[best]) = (_data[best], _data[i]);
                i = best;
            }
        }

        // Creates a clone of the priority queue
        public PriorityQueue<T> Clone()
        {
            var q = new PriorityQueue<T>();
            q._data.AddRange(_data);
            return q;
        }

        // Returns an ordered list of items from highest to lowest priority.
        public List<T> ToOrderedList()
        {
            var clone = Clone();
            var list = new List<T>();
            while (clone.Count > 0) list.Add(clone.ExtractMax());
            return list;
        }
    }
}