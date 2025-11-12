using System;
using System.Collections.Generic;

namespace MunicipalityApplicatiion.DataStructures
{
    public class Graph<T>
    {
        // Edge for MST output.
        public class Edge
        {
            public int U;
            public int V;
            public double W;
        }

        private readonly List<T> _nodes = new();
        private readonly List<List<(int v, double w)>> _adj = new();
        private readonly Dictionary<T, int> _index = new();

        // Adds a node with the given value, returns its index.
        public int AddNode(T value)
        {
            if (_index.TryGetValue(value, out var idx)) return idx;
            idx = _nodes.Count;
            _nodes.Add(value);
            _adj.Add(new List<(int v, double w)>());
            _index[value] = idx;
            return idx;
        }

        // Adds an undirected edge between nodes with values a and b.
        public void AddUndirected(T a, T b, double w)
        {
            int u = AddNode(a);
            int v = AddNode(b);
            AddUndirectedEdge(u, v, w);
        }

        // Adds an undirected edge between node indices u and v.
        public void AddUndirectedEdge(int u, int v, double w)
        {
            _adj[u].Add((v, w));
            _adj[v].Add((u, w));
        }

        // Performs BFS starting from the node at index 'start'.
        public IEnumerable<T> BfsFrom(int start)
        {
            if (_nodes.Count == 0 || start < 0 || start >= _nodes.Count)
                yield break;

            var seen = new bool[_nodes.Count];
            var q = new Queue<int>();

            q.Enqueue(start);
            seen[start] = true;

            while (q.Count > 0)
            {
                int u = q.Dequeue();
                yield return _nodes[u];

                if (u < 0 || u >= _adj.Count)
                    continue; 

                foreach (var (v, _) in _adj[u])
                {
                    if (v >= 0 && v < _nodes.Count && !seen[v])
                    {
                        seen[v] = true;
                        q.Enqueue(v);
                    }
                }
            }
        }

        // Computes the Minimum Spanning Tree using Prim's algorithm.
        public List<Edge> MinimumSpanningTree()
        {
            int n = _nodes.Count;
            var mst = new List<Edge>();
            if (n == 0) return mst;

            var inMst = new bool[n];
            var pq = new PriorityQueue<(double w, int u, int v), double>();

            // Start from node 0
            inMst[0] = true;
            foreach (var (v, w) in _adj[0]) pq.Enqueue((w, 0, v), w);

            while (pq.Count > 0 && mst.Count < n - 1)
            {
                var (w, u, v) = pq.Dequeue();
                if (inMst[v]) continue;

                inMst[v] = true;
                mst.Add(new Edge { U = u, V = v, W = w });

                foreach (var (nv, nw) in _adj[v])
                {
                    if (!inMst[nv]) pq.Enqueue((nw, v, nv), nw);
                }
            }

            return mst;
        }

    }
}
