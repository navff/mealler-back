using System.ComponentModel.DataAnnotations;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Users.Commands
{
    public class LoginOrRegisterCommand : IRequest<EntityCreatedResult>
    {
        /// <summary>
        ///     User email. User will created if there was no user
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///     Frontend url. Needed for construct login link in Email-message
        /// </summary>
        [Required]
        public string Url { get; set; }
    }
}