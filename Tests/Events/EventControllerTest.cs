using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using web.api.App.Common;
using web.api.App.Events;
using Xunit;

namespace Tests.Events
{
    public class EventControllerTest : BaseIntergationTest<EventController>
    {
        [Fact]
        public async void CreateEvent_Ok_Test()
        {
            var user = await _creator.UsersCreator.CreateOne();
            var token = Token.Create(user, _configuration);
            var team = await _creator.TeamsCreator.CreateOne();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var evt = new Event
            {
                Date = DateTime.Now,
                Name = $"Event_{Guid.NewGuid().ToString()[..5]}",
                TeamId = team.Id
            };
            var eventJson = JsonSerializer.Serialize(evt);
            var buffer = System.Text.Encoding.UTF8.GetBytes(eventJson);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync("event", byteContent);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<EntityCreatedResult>(responseJson);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.True(result?.Id > 0);
        }
    }
}