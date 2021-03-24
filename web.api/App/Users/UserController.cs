using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using web.api.App.Common;

namespace web.api.App.Users
{
    [ApiController]
    [AllowAnonymous]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult Token([FromBody] TokenRequest tokenRequest)
        {
            var token = CreateToken(tokenRequest.Email);
            if (token == null) return BadRequest(new {errorText = "Invalid username"});

            var response = new
            {
                access_token = token,
                username = tokenRequest.Email
            };

            return Ok(response);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUser()
        {
            return Ok(User.Identity.Name);
        }

        private string CreateToken(string username)
        {
            var identity = GetIdentity(username);
            if (identity == null)
            {
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetIdentity(string username)
        {
            // TODO: получить реального пользователя
            var user = new User
            {
                Email = "newUser@mail.com",
                Name = "petya",
                ActiveTeamId = 1
            };

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Email)
                    // new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                var claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }

    public class TokenRequest
    {
        public string Email { get; set; }
    }
}