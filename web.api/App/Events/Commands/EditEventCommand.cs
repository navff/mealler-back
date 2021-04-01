using System;

namespace web.api.App.Events.Commands
{
    public class EditEventCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}