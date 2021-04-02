using System;
using web.api.App.Common;

namespace web.api.App.Events.Queries
{
    public class EventSearchQuery : BaseSearchQuery<EventResponse>
    {
        public string Word { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}