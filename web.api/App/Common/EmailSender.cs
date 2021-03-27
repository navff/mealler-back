using System.IO;
using System.Text.Json;
using web.api.App.Users;

namespace web.api.App.Common
{
    public class EmailSender
    {
        public void Send(User user, string token)
        {
            var message = new SendTokenMessage
            {
                Email = user.Email,
                Token = token
            };

            File.WriteAllText("D://token_email_message.txt", JsonSerializer.Serialize(message));
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
    }
}