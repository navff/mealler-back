using System.Collections.Generic;
using web.api.App.Teams;

namespace web.api.App.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public int? ActiveTeamId { get; set; }
        public ICollection<Team> Teams { get; set; }
    }

    public enum Role
    {
        Admin = 1,
        User = 2
    }
}