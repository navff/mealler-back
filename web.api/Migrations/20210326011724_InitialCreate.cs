using Microsoft.EntityFrameworkCore.Migrations;

namespace web.api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Recipes",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    Description = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Recipes", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    Email = table.Column<string>("TEXT", nullable: true),
                    Role = table.Column<int>("INTEGER", nullable: false),
                    ActiveTeamId = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "Teams",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    OwnerUserId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        "FK_Teams_Users_OwnerUserId",
                        x => x.OwnerUserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "TeamUser",
                table => new
                {
                    MembersId = table.Column<int>("INTEGER", nullable: false),
                    TeamsId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUser", x => new {x.MembersId, x.TeamsId});
                    table.ForeignKey(
                        "FK_TeamUser_Teams_TeamsId",
                        x => x.TeamsId,
                        "Teams",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_TeamUser_Users_MembersId",
                        x => x.MembersId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Teams_OwnerUserId",
                "Teams",
                "OwnerUserId");

            migrationBuilder.CreateIndex(
                "IX_TeamUser_TeamsId",
                "TeamUser",
                "TeamsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Recipes");

            migrationBuilder.DropTable(
                "TeamUser");

            migrationBuilder.DropTable(
                "Teams");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}