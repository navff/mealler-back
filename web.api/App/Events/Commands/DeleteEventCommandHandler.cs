using System.Threading;
using System.Threading.Tasks;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Events.Commands
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, EmptyResult>
    {
        private readonly EventService _eventService;

        public DeleteEventCommandHandler(EventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<EmptyResult> Handle(DeleteEventCommand command, CancellationToken cancellationToken)
        {
            await _eventService.CheckRights(command.Id, command.Username);
            await _eventService.Delete(command.Id);
            return new EmptyResult();
        }
    }
}