using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web.api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Recipes", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    ActiveTeamId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_User", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    OwnerUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_User_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false),
                    PackPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    PackAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenceIngredients_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUser", x => new {x.UserId, x.TeamId});
                    table.ForeignKey(
                        name: "FK_TeamUser_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] {"Id", "ActiveTeamId", "Email", "Name", "Role"},
                values: new object[] {1, 1, "petya@petya-team.com", "Petya (Team Admin)", 0});

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] {"Id", "ActiveTeamId", "Email", "Name", "Role"},
                values: new object[] {2, 1, "masya@petya-team.com", "Vasya (Team member)", 0});

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] {"Id", "ActiveTeamId", "Email", "Name", "Role"},
                values: new object[] {3, 2, "tanya@petya-team.com", "Tanya (Team member)", 0});

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] {"Id", "ActiveTeamId", "Email", "Name", "Role"},
                values: new object[] {4, 3, "var@33kita.ru", "vova (Team admin and member)", 0});

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] {"Id", "Name", "OwnerUserId"},
                values: new object[] {1, "Team1", 1});

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] {"Id", "Name", "OwnerUserId"},
                values: new object[] {2, "Team2", 2});

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] {"Id", "Name", "OwnerUserId"},
                values: new object[] {3, "Team3", 4});

            migrationBuilder.InsertData(
                table: "TeamUser",
                columns: new[] {"TeamId", "UserId"},
                values: new object[] {1, 1});

            migrationBuilder.InsertData(
                table: "TeamUser",
                columns: new[] {"TeamId", "UserId"},
                values: new object[] {1, 2});

            migrationBuilder.InsertData(
                table: "TeamUser",
                columns: new[] {"TeamId", "UserId"},
                values: new object[] {2, 1});

            migrationBuilder.InsertData(
                table: "TeamUser",
                columns: new[] {"TeamId", "UserId"},
                values: new object[] {2, 3});

            migrationBuilder.InsertData(
                table: "TeamUser",
                columns: new[] {"TeamId", "UserId"},
                values: new object[] {3, 3});

            migrationBuilder.InsertData(
                table: "TeamUser",
                columns: new[] {"TeamId", "UserId"},
                values: new object[] {3, 4});

            migrationBuilder.CreateIndex(
                name: "IX_Events_TeamId",
                table: "Events",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceIngredients_TeamId",
                table: "ReferenceIngredients",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_OwnerUserId",
                table: "Team",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUser_TeamId",
                table: "TeamUser",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "ReferenceIngredients");

            migrationBuilder.DropTable(
                name: "TeamUser");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}