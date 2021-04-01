using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using web.api.App.Events;
using web.api.DataAccess;

namespace Tests.Creators
{
    public class EventsCreator : BaseCreator, ICreator<Event>
    {
        private TeamsCreator _teamsCreator;

        public EventsCreator(AppDbContext context) : base(context)
        {
            _teamsCreator = new TeamsCreator(context);
        }


        public async Task<Event> CreateOne()
        {
            var evt = new Event
            {
                Date = DateTime.Now,
                Name = $"Event_{Guid.NewGuid().ToString()[..5]}",
                TeamId = (await _teamsCreator.CreateOne()).Id
            };
            await _context.Events.AddAsync(evt);
            await _context.SaveChangesAsync();
            return evt;
        }

        public async Task<List<Event>> CreateMany(int count)
        {
            var result = new List<Event>();
            for (var i = 0; i < count; i++)
            {
                result.Add(await CreateOne());
            }

            return result;
        }
    }
}