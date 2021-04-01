using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Events.Commands
{
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand, EntityCreatedResult>
    {
        private EventService _eventService;

        public AddEventCommandHandler(EventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<EntityCreatedResult> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var evt = request.Adapt<Event>();
            var result = await _eventService.Create(evt);
            return new EntityCreatedResult(result.Id);
        }
    }
}