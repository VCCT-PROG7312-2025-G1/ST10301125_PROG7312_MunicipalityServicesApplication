using MunicipalityApplicatiion.DataStructures;
using MunicipalityApplicatiion.Models;

namespace MunicipalityApplicatiion.Repositories
{
    public static class IssueRepository
    {
        private static readonly IssueList _issues = new IssueList();
        private static int _nextId = 1;

        // Create new issue
        public static Issue Create(string location, IssueCategory category, string description, StringList attachments)
        {
            var issue = new Issue
            {
                IssueId = _nextId++,
                IssueLocation = location,
                IssueCategory = category,
                IssueDescription = description
            };
          
            attachments.ForEach(a => issue.Attachments.Add(a));

            _issues.Add(issue);
            return issue;
        }

        // Retrieve by ID
        public static int Count() => _issues.Count;

        // Iterate through all issues
        public static void ForEach(System.Action<Issue> action) => _issues.ForEach(action);
    }
}