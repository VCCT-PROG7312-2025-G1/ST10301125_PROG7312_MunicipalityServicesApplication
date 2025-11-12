using System;
using System.Collections.Generic;

namespace MunicipalityApplicatiion.DataStructures.Trees
{
    public class AvlNode<TKey, TValue> where TKey : IComparable<TKey>
    {
        public TKey Key;
        public TValue Value;
        public AvlNode<TKey, TValue>? Left;
        public AvlNode<TKey, TValue>? Right;
        public int Height = 1;

        public AvlNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public class AvlTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        public AvlNode<TKey, TValue>? Root { get; private set; }

        private int Height(AvlNode<TKey, TValue>? n) => n?.Height ?? 0;

        private int Balance(AvlNode<TKey, TValue> n) => Height(n.Left) - Height(n.Right);

        private void UpdateHeight(AvlNode<TKey, TValue> n)
        {
            n.Height = Math.Max(Height(n.Left), Height(n.Right)) + 1;
        }

        // Rotations (Right)
        private AvlNode<TKey, TValue> RotateRight(AvlNode<TKey, TValue> y)
        {
            var x = y.Left!;
            var T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        // Rotations (Left)
        private AvlNode<TKey, TValue> RotateLeft(AvlNode<TKey, TValue> x)
        {
            var y = x.Right!;
            var T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            UpdateHeight(x);
            UpdateHeight(y);
            return y;
        }

        // Insertion
        public void Insert(TKey key, TValue value)
        {
            Root = Insert(Root, key, value);
        }

        // Insertion with balancing
        private AvlNode<TKey, TValue> Insert(AvlNode<TKey, TValue>? n, TKey k, TValue v)
        {
            if (n == null)
                return new AvlNode<TKey, TValue>(k, v);

            int cmp = k.CompareTo(n.Key);
            if (cmp < 0)
                n.Left = Insert(n.Left, k, v);
            else if (cmp > 0)
                n.Right = Insert(n.Right, k, v);
            else
                n.Value = v;

            UpdateHeight(n);

            int balance = Balance(n);

            // Rotations
            if (balance > 1 && k.CompareTo(n.Left!.Key) < 0) return RotateRight(n);
            if (balance < -1 && k.CompareTo(n.Right!.Key) > 0) return RotateLeft(n);
            if (balance > 1 && k.CompareTo(n.Left!.Key) > 0)
            {
                n.Left = RotateLeft(n.Left!);
                return RotateRight(n);
            }
            if (balance < -1 && k.CompareTo(n.Right!.Key) < 0)
            {
                n.Right = RotateRight(n.Right!);
                return RotateLeft(n);
            }

            return n;
        }

        // Search
        public bool TryGet(TKey key, out TValue value)
        {
            var n = Root;
            while (n != null)
            {
                int cmp = key.CompareTo(n.Key);
                if (cmp == 0)
                {
                    value = n.Value;
                    return true;
                }
                n = cmp < 0 ? n.Left : n.Right;
            }

            value = default!;
            return false;
        }

        // In-order traversal
        public IEnumerable<TValue> InOrder()
        {
            var stack = new Stack<AvlNode<TKey, TValue>>();
            var node = Root;
            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }

                node = stack.Pop();
                yield return node.Value;
                node = node.Right;
            }
        }
    }
}