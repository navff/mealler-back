using System;

namespace web.api.App.Events
{
    public class Event
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }
    }
}