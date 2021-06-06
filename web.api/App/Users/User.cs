using System;
using System.Collections.Generic;
using System.Linq;
using web.api.App.Teams;

namespace web.api.App.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public int? ActiveTeamId { get; set; }
        public ICollection<Team> Teams { get; set; }

        public int GetTeamId()
        {
            return ActiveTeamId.HasValue
                ? ActiveTeamId.Value
                : Teams.FirstOrDefault() != null
                    ? Teams.First().Id
                    : throw new InvalidOperationException($"User `{this.Email}` must have at least one team");
        }
    }

    public static class Roles
    {
        public static string Admin = "Admin";
        public static string User = "User";
    }
}