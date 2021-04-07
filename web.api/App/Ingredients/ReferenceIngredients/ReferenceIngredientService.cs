using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using web.api.App.Common;
using web.api.App.Ingredients.ReferenceIngredients.Queries;
using web.api.DataAccess;

namespace web.api.App.Ingredients.ReferenceIngredients
{
    public class ReferenceIngredientService
        : BaseCrudService<ReferenceIngredient, ReferenceIngredient, ReferenceIngredientSearchQuery>
    {
        public ReferenceIngredientService(AppDbContext context) : base(context)
        {
        }

        public override async Task<ReferenceIngredient> Get(int id)
        {
            var ingredient = await _context.ReferenceIngredients.FirstOrDefaultAsync(e => e.Id == id);
            if (ingredient == null)
            {
                throw new EntityNotFoundException<ReferenceIngredient>(id);
            }

            return ingredient;
        }

        public override async Task<ReferenceIngredient> Create(ReferenceIngredient entity)
        {
            await _context.ReferenceIngredients.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public override async Task<ReferenceIngredient> Update(ReferenceIngredient ingredient)
        {
            var oldEntity = await Get(ingredient.Id);

            oldEntity = ingredient.Adapt(oldEntity);
            await _context.SaveChangesAsync();
            return oldEntity;
        }

        public override async Task Delete(int id)
        {
            var ingredient = await Get(id);
            _context.ReferenceIngredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public override Task<PageView<ReferenceIngredient>> Search(ReferenceIngredientSearchQuery searchParams)
        {
            var query = _context.ReferenceIngredients.AsQueryable();
            if (!string.IsNullOrEmpty(searchParams.Word))
            {
                query = query.Where(e => e.Name.ToLower().Contains(searchParams.Word.ToLower()));
            }

            return PageView<ReferenceIngredient>.GetNewInstance(query, searchParams.Page);
        }
    }
}