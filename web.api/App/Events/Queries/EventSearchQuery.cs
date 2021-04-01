using System;

namespace web.api.App.Events.Queries
{
    public class EventSearchQuery
    {
        public string Word { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}