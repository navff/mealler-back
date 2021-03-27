using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using web.api.App.Common;

namespace web.api.App.Jobs
{
    public static class HangfireConfigurationBuilder
    {
        public static void Setup(IApplicationBuilder app, IConfiguration configuration)
        {
            var hangfireConfig = configuration.GetSection("Hangfire").Get<HangfireConfig>();
            app.UseHangfireDashboard("/" + hangfireConfig.DashboardPath, new DashboardOptions
            {
                Authorization = new[]
                {
                    new BasicAuthAuthorizationFilter(
                        new BasicAuthAuthorizationFilterOptions
                        {
                            RequireSsl = false,
                            SslRedirect = false,
                            LoginCaseSensitive = true,
                            Users = new[]
                            {
                                new BasicAuthAuthorizationUser
                                {
                                    Login = hangfireConfig.AdminUsername,
                                    PasswordClear = hangfireConfig.AdminPassword
                                }
                            }
                        })
                }
            });
            app.UseHangfireServer();
        }
    }
}