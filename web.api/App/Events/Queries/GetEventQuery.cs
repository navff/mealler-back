using MediatR;

namespace web.api.App.Events.Queries
{
    public class GetEventQuery : IRequest<EventResponse>
    {
        public int Id { get; set; }
    }
}