using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.api.App.Common;
using web.api.App.Users.Commands;

namespace web.api.App.Users
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login-or-register")]
        public async Task<EntityCreatedResult> LoginOrRegister([FromBody] LoginOrRegisterCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUser()
        {
            // ReSharper disable once PossibleNullReferenceException
            return Ok(User.Identity.Name);
        }
    }
}