using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalityApplicatiion.Utils
{
    // Class to track search categories and date
    public class SearchTrack
    {
        private readonly Dictionary<string, int> _categoryCounts = new();
        private readonly Dictionary<DateTime, int> _dateCounts = new();

        // Record the category of a search
        public void RecordCategory(string? category)
        {
            if (string.IsNullOrEmpty(category) || category == "All")
                return;

            if (!_categoryCounts.ContainsKey(category))
                _categoryCounts[category] = 0;

            _categoryCounts[category]++;
        }

        // Record the date of a search
        public void RecordDate(DateTime date)
        {
            if (!_dateCounts.ContainsKey(date))
                _dateCounts[date] = 0;

            _dateCounts[date]++;
        }

        // Get the most searched category
        public string? MostSearchedCategory()
        {
            return _categoryCounts.Count == 0 ? null :
                   _categoryCounts.OrderByDescending(kv => kv.Value).First().Key;
        }

        // Get the most searched date
        public DateTime? MostSearchedDate()
        {
            return _dateCounts.Count == 0 ? null :
                   _dateCounts.OrderByDescending(kv => kv.Value).First().Key;
        }
    }
}