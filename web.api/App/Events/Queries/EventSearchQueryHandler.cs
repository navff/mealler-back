using System.Threading;
using System.Threading.Tasks;
using Common;
using Mapster;
using MediatR;

namespace web.api.App.Events.Queries
{
    public class EventSearchQueryHandler : IRequestHandler<EventSearchQuery, PageView<EventResponse>>
    {
        private readonly EventService _eventService;

        public EventSearchQueryHandler(EventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<PageView<EventResponse>> Handle(EventSearchQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventService.Search(request);
            return result.Adapt<PageView<EventResponse>>();
        }
    }
}