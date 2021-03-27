using MediatR;

namespace web.api.App.Users.Queries
{
    public class GetUserQuery : IRequest<UserResponse>
    {
        public string Email { get; set; }
    }
}