using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using web.api.App.Common;
using web.api.App.Teams;

namespace web.api.App.Ingredients.ReferenceIngredients.Commands
{
    public class
        AddReferenceIngredientCommandHandler : IRequestHandler<AddReferenceIngredientCommand, EntityCreatedResult>
    {
        private readonly ReferenceIngredientService _referenceIngredientService;
        private TeamService _teamService;

        public AddReferenceIngredientCommandHandler(TeamService teamService,
            ReferenceIngredientService referenceIngredientService)
        {
            _teamService = teamService;
            _referenceIngredientService = referenceIngredientService;
        }

        public async Task<EntityCreatedResult> Handle(AddReferenceIngredientCommand command,
            CancellationToken cancellationToken)
        {
            var ingredient = command.Adapt<ReferenceIngredient>();
            if (ingredient.TeamId == 0)
            {
                var team = await _teamService.GetActiveTeamForUser(command.Username);
                ingredient.TeamId = team.Id;
            }

            var result = await _referenceIngredientService.Create(ingredient);
            return new EntityCreatedResult(result.Id);
        }
    }
}