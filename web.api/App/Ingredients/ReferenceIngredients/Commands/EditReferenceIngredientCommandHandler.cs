using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using web.api.App.Common;
using web.api.App.Users;

namespace web.api.App.Ingredients.ReferenceIngredients.Commands
{
    public class
        EditReferenceIngredientCommandHandler : IRequestHandler<EditReferenceIngredientCommand, EntityCreatedResult>
    {
        private readonly ReferenceIngredientService _referenceIngredientService;
        private readonly UserService _userService;

        public EditReferenceIngredientCommandHandler(ReferenceIngredientService referenceIngredientService,
            UserService userService)
        {
            _referenceIngredientService = referenceIngredientService;
            _userService = userService;
        }

        public async Task<EntityCreatedResult> Handle(EditReferenceIngredientCommand command,
            CancellationToken cancellationToken)
        {
            var teamId = (await _userService.Get(command.Username)).GetTeamId();
            var ingredient = command.Adapt<ReferenceIngredient>();
            ingredient.TeamId = teamId;
            var result = await _referenceIngredientService.Update(ingredient);
            return new EntityCreatedResult(result.Id);
        }
    }
}