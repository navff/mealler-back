using System.Net;
using System.Net.Http.Headers;
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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _testAdminToken);
            var response = await _httpClient.GetAsync("user/me");
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("var@33kita.ru", result);
        }

        [Fact]
        public async void GetUser_401_Test()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "jopa");
            var response = await _httpClient.GetAsync("user/me");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}