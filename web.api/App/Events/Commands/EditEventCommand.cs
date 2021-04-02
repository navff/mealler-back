using System;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Events.Commands
{
    public class EditEventRequest : IRequest<EntityCreatedResult>
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }
    }

    public class EditEventCommand : EditEventRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}