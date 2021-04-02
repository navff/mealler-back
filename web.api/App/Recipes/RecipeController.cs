using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.api.App.Common;
using web.api.App.Recipes.Commands;

namespace web.api.App.Recipes
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RecipeController : ApiControllerBase
    {
        public RecipeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id:int}")]
        public async Task<RecipeResponse> Get(GetRecipeQuery query)
        {
            if (query.Id == 0) throw new ArgumentException("No Id!", nameof(query.Id));
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("all")]
        public IEnumerable<Recipe> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<EntityCreatedResult> Post([FromBody] AddRecipeCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}