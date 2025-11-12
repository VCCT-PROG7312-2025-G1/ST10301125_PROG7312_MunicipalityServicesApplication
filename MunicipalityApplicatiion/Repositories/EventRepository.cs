using System;
using System.Collections.Generic;
using System.Linq;
using MunicipalityApplicatiion.Models;
using MunicipalityApplicatiion.Properties;

namespace MunicipalityApplicatiion.Repositories
{
    public class EventRepository
    {
        // Data structures used for local events & announcements page features 
        private readonly SortedDictionary<DateTime, List<EventItem>> _eventsByDate = new();
        private readonly Dictionary<string, List<EventItem>> _eventsByCategory = new(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _categories = new(StringComparer.OrdinalIgnoreCase);
        private readonly PriorityQueue<EventItem, DateTime> _featuredQueue = new();
        private readonly Queue<string> _recentSearches = new();
        private readonly Stack<EventItem> _recentlyViewed = new();
        private readonly int _recentSearchLimit = 50;

        // Expose categories
        public IEnumerable<string> Categories => _categories.OrderBy(c => c);

        // Add new event
        public void AddEvent(EventItem e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));

            if (!_eventsByDate.TryGetValue(e.EventDate.Date, out var list))
            {
                list = new List<EventItem>();
                _eventsByDate[e.EventDate.Date] = list;
            }
            list.Add(e);

            if (!_eventsByCategory.TryGetValue(e.EventCategory ?? string.Empty, out var catList))
            {
                catList = new List<EventItem>();
                _eventsByCategory[e.EventCategory ?? string.Empty] = catList;
            }
            catList.Add(e);

            _categories.Add(e.EventCategory ?? string.Empty);

            if (e.IsFeatured)
                _featuredQueue.Enqueue(e, e.EventDate);
        }

        // Get all upcoming events
        public IEnumerable<EventItem> GetAllUpcomingEvents() =>
            _eventsByDate.SelectMany(kvp => kvp.Value).OrderBy(e => e.EventDate);

        // Search events by query, category, and date
        public IEnumerable<EventItem> Search(string query, string category = null, DateTime? date = null)
        {
            var q = (query ?? string.Empty).Trim();
            IEnumerable<EventItem> source;

            if (string.IsNullOrWhiteSpace(category))
            {
                source = _eventsByDate.SelectMany(kvp => kvp.Value);
            }
            else if (_eventsByCategory.TryGetValue(category, out var catList))
            {
                source = catList;
            }
            else
            {
                source = Enumerable.Empty<EventItem>();
            }

            if (date.HasValue)
                source = source.Where(e => e.EventDate.Date == date.Value.Date);

            if (!string.IsNullOrWhiteSpace(q))
            {
                source = source.Where(e =>
                    (e.EventTitle?.IndexOf(q, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                    || (e.EventDescription?.IndexOf(q, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                    || (e.EventCategory?.IndexOf(q, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0);

                RecordSearch(q);
            }

            return source.OrderBy(e => e.EventDate);
        }

        // Record recent searches
        private void RecordSearch(string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return;

            _recentSearches.Enqueue(q);

            while (_recentSearches.Count > _recentSearchLimit)
                _recentSearches.Dequeue();
        }

        // Save viewed result 
        public void MarkEventViewed(EventItem e)
        {
            if (e == null) return;
            _recentlyViewed.Push(e);
            while (_recentlyViewed.Count > 30) _recentlyViewed.Pop();
        }

        // Recommend events for user patterns & prefernces
        public IEnumerable<EventItem> Recommend(int maxRecommendations = 5)
        {
            if (!_recentSearches.Any())
            {
                if (_featuredQueue.Count > 0)
                {
                    var temp = new List<EventItem>();
                    while (_featuredQueue.Count > 0 && temp.Count < maxRecommendations)
                    {
                        _featuredQueue.TryDequeue(out var ev, out _);
                        temp.Add(ev);
                    }
                    foreach (var e in temp) _featuredQueue.Enqueue(e, e.EventDate);
                    return temp;
                }

                return _eventsByDate.SelectMany(kvp => kvp.Value).OrderBy(e => e.EventDate).Take(maxRecommendations);
            }

            var freq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var s in _recentSearches)
            {
                var terms = s.Split(new[] { ' ', ',', ';', '.', '-' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var t in terms)
                {
                    if (t.Trim().Length < 2) continue;
                    freq.TryGetValue(t, out int c);
                    freq[t] = c + 1;
                }
            }

            var topKeys = freq.OrderByDescending(kv => kv.Value).Take(4).Select(kv => kv.Key).ToList();
            var candidates = new List<EventItem>();

            foreach (var key in topKeys)
            {
                var matches = _eventsByDate.SelectMany(kvp => kvp.Value)
                    .Where(e => (e.EventTitle?.IndexOf(key, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                             || (e.EventDescription?.IndexOf(key, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                             || (e.EventCategory?.IndexOf(key, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0);
                foreach (var m in matches)
                {
                    if (!candidates.Any(c => c.EventId == m.EventId)) candidates.Add(m);
                }
            }

            if (!candidates.Any())
                candidates = _eventsByDate.SelectMany(kvp => kvp.Value).OrderBy(e => e.EventDate).Take(maxRecommendations).ToList();

            return candidates.OrderBy(e => e.EventDate).Take(maxRecommendations);
        }
    }

    // Seeded Examples of Events that are already created 
    public static class SampleData
    {
        public static EventRepository SeedEventsWithImages()
        {
            var repo = new EventRepository();

            // Event 1
            repo.AddEvent(new EventItem
            {
                EventId = 1,
                EventTitle = "COMMUNITY BEACH CLEAN UP",
                EventDescription = "Join us for a community beach clean-up for a nice day out and making your neighborhood shine!",
                EventCategory = "Environment",
                EventDate = DateTime.Today.AddDays(2),
                IsFeatured = true,
                ImagePath = @"Resources\cleanup.jpg" 
            });

            // Event 2
            repo.AddEvent(new EventItem
            {
                EventId = 2,
                EventTitle = "FOODIES FARMERS MARKET",
                EventDescription = "Fresh produce, artisan goods, and live music at the weekly farmers market.",
                EventCategory = "Market",
                EventDate = DateTime.Today.AddDays(5),
                IsFeatured = true,
                ImagePath = @"Resources/markets.jpg"
            });

            // Event 3
            repo.AddEvent(new EventItem
            {
                EventId = 3,
                EventTitle = "MUSIC AT THE PARK",
                EventDescription = "Enjoy outdoor live music with friends and family in the  park.",
                EventCategory = "Entertainment",
                EventDate = DateTime.Today.AddDays(7),
                IsFeatured = false,
                ImagePath = @"Resources\music_park.jpg"
            });

            // Event 4
            repo.AddEvent(new EventItem
            {
                EventId = 4,
                EventTitle = "HEALTHY LIFE, HEALTHY LIVING",
                EventDescription = "Free workshop on healthy living, nutrition, and exercise tips.",
                EventCategory = "Health",
                EventDate = DateTime.Today.AddDays(4),
                IsFeatured = false,
                ImagePath = @"Resources\health_workshop.jpg"
            });

            // Event 5
            repo.AddEvent(new EventItem
            {
                EventId = 5,
                EventTitle = "WHERE ART LIVES LOUD & LOCALLY",
                EventDescription = "Local artists showcase their latest works at the downtown gallery.",
                EventCategory = "Arts",
                EventDate = DateTime.Today.AddDays(6),
                IsFeatured = true,
                ImagePath = @"Resources\art_exhibition.jpg"
            });

            // Event 6 
            repo.AddEvent(new EventItem
            {
                EventId = 6,
                EventTitle = "RUN FOR FUN",
                EventDescription = "Bring your friends along, lets grow our running community, and have FUN!",
                EventCategory = "Health",
                EventDate = DateTime.Today.AddDays(6),
                IsFeatured = true,
                ImagePath = @"Resources\run.jpg"
            });

            return repo;
        }
    }
}