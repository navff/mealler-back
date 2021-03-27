using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Mapster;
using MediatR;

namespace web.api.App.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
    {
        private readonly UserService _userService;

        public GetUserQueryHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.Get(request.Email);
            if (user == null) throw new EntityNotFoundException<User>(request.Email);
            return user.Adapt<UserResponse>();
        }
    }
}