using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Tests.ToolsTests;
using web.api;
using web.api.App.Common;

namespace Tests
{
    public class BaseIntergationTest<T> : BaseUnitTest
    {
        protected readonly T _controller;
        protected readonly HttpClient _httpClient;
        protected readonly string _testAdminToken;

        public BaseIntergationTest()
        {
            var diServiceBuilder = new DIServiceBuilder();
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _httpClient = server.CreateClient();
            _controller = diServiceBuilder.GetService<T>();
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<BaseUnitTest>();
            var config = builder.Build();
            _testAdminToken = config.GetSection("Auth").Get<AuthConfig>().TestAdminToken;
        }
    }
}