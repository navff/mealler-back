using System.Collections.Generic;
using System.Linq;
using web.api.App.Recipes;
using web.api.App.Teams;
using web.api.App.Users;

namespace web.api.DataAccess
{
    public class Seeder
    {
        private readonly AppDbContext _context;

        public Seeder(AppDbContext context)
        {
            _context = context;
        }


        public void Seed()
        {
            // Recipes
            // AddRecipes();
            AddUsers();
        }

        private void AddUsers()
        {
            var petya = new User
            {
                Name = "Petya (Team Admin)",
                Email = "petya@petya-team.com"
            };

            var vasya = new User
            {
                Name = "Vasya (Team member)",
                Email = "masya@petya-team.com"
            };

            var tanya = new User
            {
                Name = "Tanya (Team member)",
                Email = "tanya@petya-team.com"
            };

            var vova = new User
            {
                Name = "vova (Team admin and member)",
                Email = "var@33kita.ru"
            };
            _context.Users.Add(petya);
            _context.Users.Add(vasya);
            _context.Users.Add(tanya);
            _context.Users.Add(vova);
            _context.SaveChanges();

            // Teams

            var petyaTeam = new Team
            {
                Name = "PetyaTeam",
                Owner = petya,
                Members = new List<User> {tanya, vasya, vova},
                OwnerUserId = petya.Id
            };

            var vovaTeam = new Team
            {
                Name = "VovaTeam",
                Owner = vova,
                Members = new List<User> {tanya, vasya},
                OwnerUserId = vova.Id
            };

            _context.Teams.Add(petyaTeam);
            _context.Teams.Add(vovaTeam);
            _context.SaveChanges();

            petya.ActiveTeamId = petyaTeam.Id;
            vasya.ActiveTeamId = petyaTeam.Id;
            vova.ActiveTeamId = vovaTeam.Id;
            tanya.ActiveTeamId = vovaTeam.Id;
            _context.SaveChanges();
        }

        private void AddRecipes()
        {
            if (_context.Recipes.Any()) return;
            _context.Add(new Recipe
            {
                Name = "Рецепт №1",
                Description = "Описание рецепта №1"
            });
            _context.Add(new Recipe
            {
                Name = "Рецепт №2",
                Description = "Описание рецепта №2"
            });
            _context.Add(new Recipe
            {
                Name = "Рецепт №3",
                Description = "Неожиданно ещё одно описание"
            });
            _context.Add(new Recipe
            {
                Name = "Рецепт №4",
                Description = "А тут описание с буквы А"
            });
            _context.Add(new Recipe
            {
                Name = "Рецепт №5",
                Description = "Бля! Ещё один рецепт"
            });
            _context.SaveChanges();
        }
    }
}