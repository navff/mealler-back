using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tests.ToolsTests;
using web.api.App.Events;
using web.api.App.Events.Queries;
using Xunit;

namespace Tests.Events
{
    public class EventServiceTests : BaseUnitTest
    {
        private readonly EventService _eventService;

        public EventServiceTests()
        {
            var diServiceBuilder = new DIServiceBuilder();
            _eventService = diServiceBuilder.GetService<EventService>();
        }

        [Fact]
        public async void AddEvent_Ok_Test()
        {
            var team = await _creator.TeamsCreator.CreateOne();
            var evt = new Event
            {
                Date = DateTime.Now,
                Name = Guid.NewGuid().ToString(),
                TeamId = team.Id
            };
            var result = await _eventService.Create(evt);
            Assert.True(result.Id != 0);
        }

        [Fact]
        public async void AddEvent_WrongTeam_Test()
        {
            var evt = new Event
            {
                Date = DateTime.Now,
                Name = Guid.NewGuid().ToString(),
                TeamId = 9999999 // <-- This is wrong!
            };
            await Assert.ThrowsAsync<DbUpdateException>(async () => { await _eventService.Create(evt); });
        }

        [Fact]
        public async void GetEvent_OK_Test()
        {
            var evt = await _creator.EventsCreator.CreateOne();
            var result = await _eventService.Get(evt.Id);
            Assert.Equal(evt.Id, result.Id);
        }

        [Fact]
        public async void SearchEvent_OK_Test()
        {
            var events = await _creator.EventsCreator.CreateMany(6);
            var result = await _eventService.Search(new EventSearchQuery
            {
                Word = events.First().Name[..4].ToUpper(),
                FromDate = DateTime.Now.AddDays(-1),
                ToDate = DateTime.Now.AddDays(1)
            });
            Assert.True(result.ItemsCount > 0);
        }

        [Fact]
        public async void EditEvent_OK_Test()
        {
            var evt = await _creator.EventsCreator.CreateOne();
            var modified = new Event()
            {
                Date = DateTime.Now,
                Name = "Event_" + Guid.NewGuid().ToString()[..5],
                TeamId = evt.TeamId,
                Id = evt.Id
            };
            await _eventService.Update(modified);

            var updated = await _eventService.Get(modified.Id);
            Assert.Equal(modified.Name, updated.Name);
            Assert.Equal(modified.Id, updated.Id);
            Assert.Equal(modified.Date, updated.Date);
            Assert.Equal(modified.TeamId, updated.TeamId);
        }
    }
}