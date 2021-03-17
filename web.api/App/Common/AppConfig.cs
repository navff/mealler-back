namespace web.api.App.Common
{
    public class AppConfig
    {
        public AuthConfig Auth { get; set; }
    }

    public class AuthConfig
    {
        public GoogleAuthConfig Google { get; set; }
    }

    public class GoogleAuthConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}