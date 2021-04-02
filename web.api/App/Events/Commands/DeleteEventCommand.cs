using MediatR;
using web.api.App.Common;

namespace web.api.App.Events.Commands
{
    public class DeleteEventRequest : IRequest<EmptyResult>
    {
        public int Id { get; set; }
    }

    public class DeleteEventCommand : DeleteEventRequest
    {
        public string Username { get; set; }
    }
}