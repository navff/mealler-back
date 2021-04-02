using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;

namespace web.api.App.Events.Queries
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventResponse>
    {
        private EventService _eventService;

        public GetEventQueryHandler(EventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<EventResponse> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventService.Get(request.Id);
            return result.Adapt<EventResponse>();
        }
    }
}