using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using web.api.App.Common;
using web.api.DataAccess;

namespace web.api.App.Users
{
    public class UserService : BaseCrudService<User, User, UserSearchParams>
    {
        public UserService(AppDbContext context) : base(context)
        {
        }

        public override async Task<User> Get(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw new EntityNotFoundException<User>(id);

            return user;
        }

        public async Task<User> Get(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public override async Task<User> Create(User entity)
        {
            // create user and send message to email
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            // TODO: send email

            return entity;
        }

        public override Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public override Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<PageView<User>> Search(UserSearchParams searchParams)
        {
            var query = _context.Users
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.Word))
            {
                query = query.Where(u => u.Email.ToLower().Contains(searchParams.Word.ToLower()));
                query = query.Union(query.Where(u => u.Name.ToLower().Contains(searchParams.Word.ToLower())));
            }

            return await PageView<User>.GetNewInstance(query);
        }
    }
}