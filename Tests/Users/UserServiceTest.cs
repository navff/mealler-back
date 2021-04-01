using Tests.ToolsTests;
using web.api.App.Users;
using Xunit;

namespace Tests.Users
{
    public class UserServiceTest : BaseUnitTest
    {
        private readonly UserService _userService;

        public UserServiceTest()
        {
            var diServiceBuilder = new DIServiceBuilder();
            _userService = diServiceBuilder.GetService<UserService>();
        }

        [Fact]
        public async void GetById_Test()
        {
            var user = await _creator.UsersCreator.CreateOne();

            var result = await _userService.Get(user.Id);

            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async void GetByEmail_Test()
        {
            var user = await _creator.UsersCreator.CreateOne();

            var result = await _userService.Get(user.Email);

            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async void GetByWrongEmail_should_return_null_Test()
        {
            var result = await _userService.Get("this is wrong email");
            Assert.Null(result);
        }

        [Fact]
        public async void SearchByEmail_Test()
        {
            await _creator.UsersCreator.CreateMany(5);

            var result = await _userService.Search(new UserSearchParams {Word = "hohoho"});
            // some other test can create users too
            Assert.True(result.ItemsCount >= 5);
        }
    }
}