using System;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Events.Commands
{
    public class AddEventRequest : IRequest<EntityCreatedResult>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }

    public class AddEventCommand : AddEventRequest, IUserCommand
    {
        public string Username { get; set; }
    }
}