using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using web.api.App.Common;
using web.api.App.Users;
using web.api.DataAccess;

namespace web.api.App.Teams
{
    public class TeamService : BaseCrudService<Team, Team, TeamSearchQuery>
    {
        public TeamService(AppDbContext context) : base(context)
        {
        }

        public override async Task<Team> Get(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (team == null)
            {
                throw new EntityNotFoundException<Team>(id);
            }

            return team;
        }

        public async Task<Team> GetActiveTeamForUser(string username)
        {
            var users = _context.Users.ToList();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == username.ToLower());

            if (user == null)
            {
                throw new EntityNotFoundException<User>(username);
            }

            if (!user.ActiveTeamId.HasValue)
            {
                throw new InvalidOperationException("There is no ActiveTeam for user");
            }

            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == user.ActiveTeamId);
            if (team == null)
            {
                throw new EntityNotFoundException<Team>(user.ActiveTeamId.Value);
            }

            return team;
        }

        public override Task<Team> Create(Team entity)
        {
            throw new System.NotImplementedException();
        }

        public override Task<Team> Update(Team evt)
        {
            throw new System.NotImplementedException();
        }

        public override Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public override Task<PageView<Team>> Search(TeamSearchQuery searchParams)
        {
            throw new System.NotImplementedException();
        }

        public override Task CheckRights(int id, string username)
        {
            throw new NotImplementedException();
        }
    }
}