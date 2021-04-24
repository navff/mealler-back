using System.Collections.Generic;
using web.api.App.Teams;

namespace web.api.App.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public int? ActiveTeamId { get; set; }
        public List<TeamShortResponse> Teams { get; set; }
    }
}