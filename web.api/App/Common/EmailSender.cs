using System.IO;
using web.api.App.Users;

namespace web.api.App.Common
{
    public class EmailSender
    {
        public void Send(User user, string token, string frontendUrl)
        {
            var message = new SendTokenMessage
            {
                Email = user.Email,
                Token = token,
                FrontendUrl = frontendUrl
            };

            File.WriteAllText("D://token_email_message.txt", message.ToString());
        }
    }

    public interface IEmailMessage
    {
        public string ToString();
    }

    public class SendTokenMessage : IEmailMessage
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string FrontendUrl { get; set; }

        public override string ToString()
        {
            var frontUrl = $"{FrontendUrl}#login/{Token}";
            return "Перейдите по ссылке, чтобы попасть внутрь:</br>" +
                   $"<a href=\"{frontUrl}\">Волшебная ссылка</a>";
        }
    }
}