using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.api.App.Common;
using web.api.App.Events.Commands;
using web.api.App.Events.Queries;

namespace web.api.App.Events
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class EventController : ControllerBase,
        ICrudController<
            GetEventQuery,
            AddEventCommand,
            EditEventCommand,
            DeleteEventCommand,
            EventSearchQuery>
    {
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(EventResponse), 200)]
        public Task<ObjectResult> Get(GetEventQuery query)
        {
            throw new System.NotImplementedException();
        }

        [HttpPost()]
        public Task<ObjectResult> Post(AddEventCommand command)
        {
            throw new System.NotImplementedException();
        }

        [HttpPut("{id:int}")]
        public Task<ObjectResult> Put(int id, EditEventCommand command)
        {
            throw new System.NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public Task<ObjectResult> Delete(DeleteEventCommand command)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet("search")]
        public Task<ObjectResult> Search(EventSearchQuery searchParams)
        {
            throw new System.NotImplementedException();
        }
    }
}