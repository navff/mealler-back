using MediatR;
using web.api.App.Common;

namespace web.api.App.Users.Commands
{
    public class LoginOrRegisterCommand : IRequest<EntityCreatedResult>
    {
        public string Email { get; set; }
    }
}