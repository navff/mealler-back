using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
                var token = CreateToken(existingUser);
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
                Email = email
            });

            var token = CreateToken(newUser);
            // TODO: send token to email

            return new EntityCreatedResult(newUser.Id);
        }

        private string CreateToken(User user)
        {
            var identity = GetIdentity(user);
            var now = DateTime.UtcNow;
            var authConfig = _configuration.GetSection("Auth").Get<AuthConfig>();
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(authConfig.SecretJwtKey),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}