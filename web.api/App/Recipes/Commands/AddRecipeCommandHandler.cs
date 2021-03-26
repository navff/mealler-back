using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Recipes.Commands
{
    public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, EntityCreatedResult>
    {
        private readonly RecipeService _recipeService;

        public AddRecipeCommandHandler(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }


        public async Task<EntityCreatedResult> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipeModel = request.Adapt<Recipe>();
            var resultRecipe = await _recipeService.Add(recipeModel);

            return new EntityCreatedResult(resultRecipe.Id);
        }
    }
}