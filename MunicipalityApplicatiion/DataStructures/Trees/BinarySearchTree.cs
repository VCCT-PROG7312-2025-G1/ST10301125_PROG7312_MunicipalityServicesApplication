using System;
using System.Collections.Generic;

namespace MunicipalityApplicatiion.DataStructures
{
    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        // Internal node
        public class Node
        {
            public TKey Key;
            public TValue Value;
            public Node? Left;
            public Node? Right;

            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private Node? _root;

        // Insert or update
        public void Insert(TKey key, TValue value)
        {
            _root = InsertInternal(_root, key, value);
        }

        // Insert helper
        private Node InsertInternal(Node? node, TKey key, TValue value)
        {
            if (node == null)
                return new Node(key, value);

            int cmp = key.CompareTo(node.Key);
            if (cmp < 0)
                node.Left = InsertInternal(node.Left, key, value);
            else if (cmp > 0)
                node.Right = InsertInternal(node.Right, key, value);
            else
                node.Value = value; 

            return node;
        }

        // Repository lookup
        public bool TryGetValue(TKey key, out TValue value)
        {
            var cur = _root;
            while (cur != null)
            {
                int cmp = key.CompareTo(cur.Key);
                if (cmp == 0)
                {
                    value = cur.Value;
                    return true;
                }
                cur = (cmp < 0) ? cur.Left : cur.Right;
            }

            value = default!;
            return false;
        }

        // In-order traversal
        public IEnumerable<TValue> InOrder()
        {
            var stack = new Stack<Node>();
            var node = _root;

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

        // In-order traversal with keys
        public IEnumerable<(TKey Key, TValue Value)> InOrderPairs()
        {
            var stack = new Stack<Node>();
            var node = _root;

            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }

                node = stack.Pop();
                yield return (node.Key, node.Value);
                node = node.Right;
            }
        }
    }
}
