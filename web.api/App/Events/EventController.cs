using System.Threading.Tasks;
using Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.api.App.Common;
using web.api.App.Events.Commands;
using web.api.App.Events.Queries;

namespace web.api.App.Events
{
    [Authorize]
    public class EventController : ApiControllerBase,
        ICrudController<
            GetEventQuery,
            AddEventRequest,
            EditEventRequest,
            DeleteEventRequest,
            EventSearchQuery>
    {
        public EventController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(EventResponse), 200)]
        public async Task<ObjectResult> Get(GetEventQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(EntityCreatedResult), 200)]
        public async Task<ObjectResult> Post([FromBody] AddEventRequest request)
        {
            var command = request.Adapt<AddEventCommand>();
            command.Username = Request.HttpContext.User?.Identity?.Name;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(EntityCreatedResult), 200)]
        public async Task<ObjectResult> Put(int id, [FromBody] EditEventRequest request)
        {
            var command = request.Adapt<EditEventCommand>();
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ObjectResult> Delete([FromRoute] DeleteEventRequest request)
        {
            var command = request.Adapt<DeleteEventCommand>();
            command.Username = Request.HttpContext.User?.Identity?.Name;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PageView<EventResponse>), 200)]
        public async Task<ObjectResult> Search([FromQuery] EventSearchQuery searchParams)
        {
            var result = await _mediator.Send(searchParams);
            return Ok(result);
        }
    }
}