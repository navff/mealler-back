using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using web.api.App.Common;
using web.api.App.Events.Queries;
using web.api.DataAccess;

namespace web.api.App.Events
{
    public class EventService : BaseCrudService<Event, Event, EventSearchQuery>
    {
        public EventService(AppDbContext context) : base(context)
        {
        }

        public override async Task<Event> Get(int id)
        {
            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null)
            {
                throw new EntityNotFoundException<Event>(id);
            }

            return evt;
        }

        public override async Task<Event> Create(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public override async Task<Event> Update(Event evt)
        {
            var oldEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == evt.Id);
            if (oldEntity == null)
            {
                throw new EntityNotFoundException<Event>(evt.Id);
            }

            oldEntity = evt.Adapt(oldEntity, MapsterConfig.Ignore("TeamId"));
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public override async Task Delete(int id)
        {
            var evt = await Get(id);
            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
        }

        public override Task<PageView<Event>> Search(EventSearchQuery searchParams)
        {
            var query = _context.Events.AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.Word))
            {
                query = query.Where(e => e.Name.ToLower().Contains(searchParams.Word.ToLower()));
            }

            if (searchParams.FromDate.HasValue)
            {
                query = query.Where(e => e.Date > searchParams.FromDate);
            }

            if (searchParams.ToDate.HasValue)
            {
                query = query.Where(e => e.Date < searchParams.ToDate);
            }

            return PageView<Event>.GetNewInstance(query, searchParams.Page);
        }
    }
}