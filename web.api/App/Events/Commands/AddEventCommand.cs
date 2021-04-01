using System;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Events.Commands
{
    public class AddEventCommand : IRequest<EntityCreatedResult>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}