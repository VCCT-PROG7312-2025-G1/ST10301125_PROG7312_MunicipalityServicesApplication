using System;

namespace MunicipalityApplicatiion.Models
{
    public enum RequestStatus { Submitted, InProgress, OnHold, Completed, Cancelled }

    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public string RequestId { get; set; } = Guid.NewGuid().ToString("N"); // unique identifier
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; } // 1 = highest priority
        public RequestStatus Status { get; set; } = RequestStatus.Submitted;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string LocationNode { get; set; } = ""; // e.g., "Cape Town", "City Centre", "School", in the Graph

        // Implement IComparable for sorting by RequestId
        public int CompareTo(ServiceRequest? other)
        => other == null ? 1 : string.Compare(RequestId, other.RequestId, StringComparison.Ordinal);

        // Override ToString for easy display
        public override string ToString()
        => $"[{RequestId}] {Title} | P{Priority} | {Status} | {LocationNode}";
    }
}