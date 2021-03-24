using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace web.api.App.Common
{
    public class AuthOptions
    {
        public const string ISSUER = "MeallerApiServer"; // издатель токена
        public const string AUDIENCE = "MeallerClient"; // потребитель токена
        private const string KEY = "mysupersecret_secretkey!123"; // ключ для шифрации
        public const int LIFETIME = 100; // время жизни токена в минутах

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            // TODO: Достать KEY из Configuration
            return new(Encoding.ASCII.GetBytes(KEY));
        }
    }
}