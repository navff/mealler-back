using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.App.Teams;
using web.api.App.Users;

namespace web.api.DataAccess.Configurations
{
    public class TeamDbConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Team");

            builder
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(z => z.OwnerUserId);


            builder.HasData(
                new Team
                {
                    Id = 1,
                    Name = "Team1",
                    OwnerUserId = 1
                },
                new Team
                {
                    Id = 2,
                    Name = "Team2",
                    OwnerUserId = 2
                },
                new Team
                {
                    Id = 3,
                    Name = "Team3",
                    OwnerUserId = 4
                }
            );


            builder
                .HasMany(p => p.Members)
                .WithMany(p => p.Teams)
                .UsingEntity<Dictionary<string, object>>("TeamUser",
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    l => l.HasOne<Team>().WithMany().HasForeignKey("TeamId"),
                    je =>
                    {
                        je.HasKey("UserId", "TeamId");
                        je.HasData(
                            new {UserId = 1, TeamId = 1},
                            new {UserId = 1, TeamId = 2},
                            new {UserId = 2, TeamId = 1},
                            new {UserId = 3, TeamId = 2},
                            new {UserId = 3, TeamId = 3},
                            new {UserId = 4, TeamId = 3});
                    });
        }
    }
}