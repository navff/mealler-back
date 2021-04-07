using System;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using web.api.App.Common;
using web.api.DataAccess;

namespace web.api.App.Recipes
{
    public class RecipeService : BaseCrudService<Recipe, Recipe, RecipeSearchParams>
    {
        public RecipeService(AppDbContext context) : base(context)
        {
        }

        public async Task<Recipe> Add(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public override async Task<Recipe> Get(int id)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null) throw new EntityNotFoundException<Recipe>(id);
            return recipe;
        }

        public override Task<Recipe> Create(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Recipe> Update(Recipe evt)
        {
            throw new NotImplementedException();
        }

        public override Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<PageView<Recipe>> Search(RecipeSearchParams searchParams)
        {
            throw new NotImplementedException();
        }
    }
}