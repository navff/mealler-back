using System.Threading.Tasks;
using Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.api.App.Common;
using web.api.App.Ingredients.ReferenceIngredients.Commands;
using web.api.App.Ingredients.ReferenceIngredients.Queries;

namespace web.api.App.Ingredients.ReferenceIngredients
{
    [ApiController]
    [Route("[controller]")]
    public class ReferenceIngredientController : ApiControllerBase,
        ICrudController<
            GetReferenceIngredientQuery,
            AddReferenceIngredientRequest,
            EditReferenceIngredientRequest,
            DeleteReferenceIngredientRequest,
            ReferenceIngredientSearchQuery>
    {
        public ReferenceIngredientController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ReferenceIngredientResponse), 200)]
        public async Task<ObjectResult> Get(GetReferenceIngredientQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost()]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(EntityCreatedResult), 200)]
        public async Task<ObjectResult> Post([FromBody] AddReferenceIngredientRequest request)
        {
            var command = request.Adapt<AddReferenceIngredientCommand>();
            command.Username = Request.HttpContext.User?.Identity?.Name;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(EntityCreatedResult), 200)]
        public async Task<ObjectResult> Put(int id, [FromBody] EditReferenceIngredientRequest request)
        {
            var command = request.Adapt<EditReferenceIngredientCommand>();
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        public async Task<ObjectResult> Delete(DeleteReferenceIngredientRequest request)
        {
            var command = request.Adapt<DeleteReferenceIngredientCommand>();
            command.Username = Request.HttpContext.User?.Identity?.Name;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PageView<ReferenceIngredientResponse>), 200)]
        public async Task<ObjectResult> Search(ReferenceIngredientSearchQuery searchParams)
        {
            var result = await _mediator.Send(searchParams);
            return Ok(result);
        }
    }
}