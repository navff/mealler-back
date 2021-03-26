namespace web.api.App.Common
{
    public class AppConfig
    {
        public AuthConfig Auth { get; set; }
    }

    public class AuthConfig
    {
        public string TestAdminToken { get; set; }
        public string SecretJwtKey { get; set; }
    }
}