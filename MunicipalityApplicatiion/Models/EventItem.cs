using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApplicatiion.Models
{
    public class EventItem
    {
        public int EventId { get; set; } // unique identifier
        public required string EventTitle { get; set; }
        public string? EventDescription { get; set; }
        public required string EventCategory { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsFeatured { get; set; }
        public string? ImagePath { get; set; }
    }
}