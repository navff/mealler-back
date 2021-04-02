using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Events.Commands
{
    public class EditEventCommandHandler : IRequestHandler<EditEventCommand, EntityCreatedResult>
    {
        private readonly EventService _eventService;

        public EditEventCommandHandler(EventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<EntityCreatedResult> Handle(EditEventCommand command, CancellationToken cancellationToken)
        {
            var eventModel = command.Adapt<Event>();
            var result = await _eventService.Update(eventModel);
            return new EntityCreatedResult(result.Id);
        }
    }
}