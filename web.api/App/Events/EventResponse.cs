using System;

namespace web.api.App.Events
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}