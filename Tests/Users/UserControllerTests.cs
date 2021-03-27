using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using web.api.App.Common;
using web.api.App.Users;
using web.api.App.Users.Commands;
using Xunit;

namespace Tests.Users
{
    public class UserControllerTests : BaseIntergationTest<UserController>
    {
        [Fact]
        public async void CreateUser_Ok_Test()
        {
            var result = await _controller.LoginOrRegister(new LoginOrRegisterCommand
            {
                Email = "jojojo@mail.com"
            });
            Assert.True(result.Id != 0);
        }

        [Fact]
        public async void GetUser_Ok_Test()
        {
            var user = await _creator.UsersCreator.CreateOne();
            var token = Token.Create(user, _configuration);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("user/me");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<UserResponse>();
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async void GetUser_401_Test()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "jopa");
            var response = await _httpClient.GetAsync("user/me");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void GetUser_404_Test()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _testAdminToken);
            var response = await _httpClient.GetAsync("user/error-for-test", HttpCompletionOption.ResponseHeadersRead);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}