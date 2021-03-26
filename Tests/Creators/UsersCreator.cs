using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using web.api.App.Users;
using web.api.DataAccess;

namespace Tests.Creators
{
    public class UsersCreator : BaseCreator, ICreator<User>
    {
        public UsersCreator(AppDbContext context) : base(context)
        {
        }

        public async Task<User> CreateOne()
        {
            var user = new User
            {
                Email = Guid.NewGuid().ToString()[..9] + "mailmailmail.ru",
                Name = "Lololol"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> CreateMany(int count)
        {
            var createdUsers = new List<User>();
            for (var i = 0; i < count; i++)
            {
                var user = new User
                {
                    Email = $"user{i}@lololol-hohoho.com",
                    Name = $"user{i}"
                };
                createdUsers.Add(user);
            }

            await _context.Users.AddRangeAsync(createdUsers);
            await _context.SaveChangesAsync();
            return createdUsers;
        }
    }
}