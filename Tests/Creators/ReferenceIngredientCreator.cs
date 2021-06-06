using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using web.api.App.Common;
using web.api.App.Ingredients.ReferenceIngredients;
using web.api.DataAccess;

namespace Tests.Creators
{
    public class ReferenceIngredientCreator : BaseCreator, ICreator<ReferenceIngredient>
    {
        private TeamsCreator _teamsCreator;

        public ReferenceIngredientCreator(AppDbContext context) : base(context)
        {
            _teamsCreator = new TeamsCreator(context);
        }

        public async Task<ReferenceIngredient> CreateOne()
        {
            var ingredient = new ReferenceIngredient()
            {
                Name = $"Ingredient_{Guid.NewGuid().ToString()[..5]}",
                Price = 12,
                Unit = Unit.gr,
                PackAmount = 800,
                PackPrice = 76,
                TeamId = (await _teamsCreator.CreateOne()).Id
            };
            await _context.ReferenceIngredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task<List<ReferenceIngredient>> CreateMany(int count)
        {
            var result = new List<ReferenceIngredient>();
            for (var i = 0; i < count; i++)
            {
                result.Add(await CreateOne());
            }

            return result;
        }
    }
}