using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using web.api.App.Events;
using web.api.DataAccess;

namespace web.api.App.Common
{
    public abstract class BaseCrudService<T, T_SearchResult, T_SearchParams>
    {
        protected AppDbContext _context;

        protected BaseCrudService(AppDbContext context)
        {
            _context = context;
        }

        public abstract Task<T> Get(int id);
        public abstract Task<T> Create(T entity);
        public abstract Task<T> Update(T evt);
        public abstract Task Delete(int id);
        public abstract Task<PageView<T_SearchResult>> Search(T_SearchParams searchParams);

        public async Task CheckRights(int id, string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new NoAuthenticationException($"You are not authenticated.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == username.ToLower());
            if (user == null)
            {
                throw new NoAuthenticationException($"You are not authenticated. Username={username}");
            }

            // to check if NotFound
            await Get(id);

            var userTeams = _context.Teams.Where(t =>
                (t.OwnerUserId == user.Id) ||
                (t.Members.Contains(user)));
            if (userTeams == null || !userTeams.Any())
            {
                throw new ForbiddenAccessException<Event>(id);
            }
        }
    }
}