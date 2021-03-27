namespace web.api.App.Common
{
    public class AppConfig
    {
        public AuthConfig Auth { get; set; }
        public EmailConfig Email { get; set; }
    }

    public class AuthConfig
    {
        public string TestAdminToken { get; set; }
        public string SecretJwtKey { get; set; }
    }

    public class EmailConfig
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class HangfireConfig
    {
        public string DashboardPath { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
    }
}