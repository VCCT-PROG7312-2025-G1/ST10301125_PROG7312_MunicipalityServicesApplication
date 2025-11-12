using MunicipalityApplicatiion.DataStructures;
using MunicipalityApplicatiion.DataStructures.Trees;
using MunicipalityApplicatiion.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalityApplicatiion.Repositories
{
    public class ServiceRequestRepository
    {
        private readonly BinarySearchTree<string, ServiceRequest> _bstById = new();
        private readonly AvlTree<long, ServiceRequest> _avlByTimeKey = new();
        private readonly RedBlackTree<string, ServiceRequest> _rbtByTitle = new();
        private readonly PriorityQueue<ServiceRequest> _priorityQueue = new();
        private readonly Graph<string> _areaGraph = new();

        private readonly List<ServiceRequest> _all;

        // Forms can react to repository changes
        public event EventHandler? Changed;

        // Helper to safely raise the event
        private void OnChanged() => Changed?.Invoke(this, EventArgs.Empty);

        public ServiceRequestRepository(IEnumerable<ServiceRequest> requests = null)
        {
            _all = requests?.ToList() ?? new List<ServiceRequest>();

            if (!_all.Any())
                SeedDemoData();

            Build();
        }

        // Seed with demo data of 15 reports
        private void SeedDemoData()
        {
            var rand = new Random();
            var locations = new[] { "Cape Town", "City Centre", "School", "Hospital", "Station" };
            var categories = new[] { "Sanitation", "Roads", "Utilities", "Safety", "Other" };

            for (int i = 1; i <= 15; i++)
            {
                var req = new ServiceRequest
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Title = $"{categories[rand.Next(categories.Length)]} issue #{i}",
                    Description = $"This is a demo description for issue #{i}.",
                    Priority = rand.Next(1, 5),
                    Status = (RequestStatus)rand.Next(0, Enum.GetValues(typeof(RequestStatus)).Length),
                    LocationNode = locations[rand.Next(locations.Length)],
                    CreatedAt = DateTime.Now.AddDays(-rand.Next(30)),
                    UpdatedAt = DateTime.Now.AddDays(-rand.Next(10)) 
                };

                _all.Add(req);
            }
        }

        // CRUD Operations
        public void Add(ServiceRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RequestId))
                throw new ArgumentException("Invalid service request.");

            _all.Add(request);
            _bstById.Insert(request.RequestId, request);
            _avlByTimeKey.Insert(TimeKey(request.CreatedAt, request.RequestId), request);
            _rbtByTitle.Insert(request.Title ?? string.Empty, request);
            _priorityQueue.Insert(request, request.Priority);

            OnChanged(); 
        }

        // Update existing request
        public void Update(ServiceRequest updated)
        {
            if (updated == null || string.IsNullOrEmpty(updated.RequestId))
                throw new ArgumentException("Invalid service request.");

            var existing = _all.FirstOrDefault(x => x.RequestId == updated.RequestId);
            if (existing != null)
            {
                _all.Remove(existing);
                _all.Add(updated);

                _priorityQueue.Clear();
                Build();

                OnChanged(); 
            }
        }

        // Delete request by ID
        public void Delete(string requestId)
        {
            var existing = _all.FirstOrDefault(x => x.RequestId == requestId);
            if (existing != null)
            {
                _all.Remove(existing);
             
                _priorityQueue.Clear();
                Build();

                OnChanged(); 
            }
        }

        // Build data structures from the current list
        private void Build()
        {
            foreach (var r in _all)
            {
                _bstById.Insert(r.RequestId, r);
                _avlByTimeKey.Insert(TimeKey(r.CreatedAt, r.RequestId), r);
                _rbtByTitle.Insert(r.Title ?? string.Empty, r);
                _priorityQueue.Insert(r, r.Priority);
            }

            var locations = _all.Select(r => r.LocationNode ?? "Unknown").Distinct().ToList();
            var indices = new Dictionary<string, int>();

            for (int i = 0; i < locations.Count; i++)
                indices[locations[i]] = _areaGraph.AddNode(locations[i]);

            for (int i = 1; i < locations.Count; i++)
            {
                int u = indices[locations[i - 1]];
                int v = indices[locations[i]];
                _areaGraph.AddUndirectedEdge(u, v, 1.0);
            }
        }

        // Generate a unique time-based key
        private static long TimeKey(DateTime dt, string id)
        {
            return dt.Ticks * 1000 + (id.GetHashCode() & 0xFFF);
        }

        // Query Operations
        public bool TryFindById(string requestId, out ServiceRequest request) =>
            _bstById.TryGetValue(requestId, out request);

        // Alias for TryFindById
        public bool TryGet(string requestId, out ServiceRequest request) =>
            TryFindById(requestId, out request);

        // In-order traversal by creation date
        public IEnumerable<ServiceRequest> InOrderByCreatedDate() => _avlByTimeKey.InOrder();

        // Find by title
        public bool TryFindByTitle(string title, out ServiceRequest request) =>
            _rbtByTitle.TryGet(title ?? string.Empty, out request);

        // Alias for Most Urgent request (priority queue)
        public IEnumerable<ServiceRequest> MostUrgent(int max = 5)
        {
            var result = new List<ServiceRequest>();
            var tempQueue = _priorityQueue.Clone();

            for (int i = 0; i < max && tempQueue.Count > 0; i++)
                result.Add(tempQueue.ExtractMax());

            return result;
        }

        // Graph operations
        public IEnumerable<string> AreaBfs()
        {
            if (!_all.Any()) return Enumerable.Empty<string>();
            return _areaGraph.BfsFrom(0);
        }

        // Minimum Spanning Tree edges
        public IEnumerable<(string U, string V, double W)> AreaMst()
        {
            var edges = _areaGraph.MinimumSpanningTree();
            return edges.Select(e => (U: e.U.ToString(), V: e.V.ToString(), W: e.W));
        }

        // Get all requests
        public IEnumerable<ServiceRequest> All() => _all;
    }
}