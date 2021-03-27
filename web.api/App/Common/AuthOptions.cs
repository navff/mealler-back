using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace web.api.App.Common
{
    public class AuthOptions
    {
        public const string ISSUER = "MeallerApiServer"; // издатель токена
        public const string AUDIENCE = "MeallerClient"; // потребитель токена
        public const int LIFETIME = 60 * 24 * 365; // время жизни токена в минутах

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKeyFromConfig)
        {
            return new(Encoding.ASCII.GetBytes(secretKeyFromConfig));
        }
    }
}