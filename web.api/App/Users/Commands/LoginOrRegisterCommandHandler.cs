using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using web.api.App.Common;

namespace web.api.App.Users.Commands
{
    public class LoginOrRegisterCommandHandler : IRequestHandler<LoginOrRegisterCommand, EntityCreatedResult>
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public LoginOrRegisterCommandHandler(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<EntityCreatedResult> Handle(LoginOrRegisterCommand request,
            CancellationToken cancellationToken)
        {
            User existingUser;

            try
            {
                existingUser = await _userService.Get(request.Email);
            }
            catch (Exception e)
            {
                return new EntityCreatedResult(e);
            }

            if (existingUser != null)
            {
                // TODO: send token to email
                var token = Token.Create(existingUser, _configuration);
                return new EntityCreatedResult(existingUser.Id);
            }

            // if there is no such user, create it
            try
            {
                return await CreateUserAndSendTokenToEmail(request.Email);
            }
            catch (Exception e)
            {
                return new EntityCreatedResult(e);
            }
        }

        private async Task<EntityCreatedResult> CreateUserAndSendTokenToEmail(string email)
        {
            var newUser = await _userService.Create(new User
            {
                Email = email,
                Role = Role.User
            });

            var token = Token.Create(newUser, _configuration);
            // TODO: send token to email

            return new EntityCreatedResult(newUser.Id);
        }
    }
}