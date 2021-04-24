using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.App.Users;

namespace web.api.DataAccess.Configurations
{
    public class UserDbConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasData
            (
                new User
                {
                    Id = 1,
                    Name = "Petya (Team Admin)",
                    Email = "petya@petya-team.com",
                    ActiveTeamId = 1
                },
                new User
                {
                    Id = 2,
                    Name = "Vasya (Team member)",
                    Email = "masya@petya-team.com",
                    ActiveTeamId = 1
                },
                new User
                {
                    Id = 3,
                    Name = "Tanya (Team member)",
                    Email = "tanya@petya-team.com",
                    ActiveTeamId = 2
                },
                new User
                {
                    Id = 4,
                    Name = "vova (Team admin and member!)",
                    Email = "var@33kita.ru",
                    ActiveTeamId = 3,
                    Role = "Admin"
                }
            );
        }
    }
}