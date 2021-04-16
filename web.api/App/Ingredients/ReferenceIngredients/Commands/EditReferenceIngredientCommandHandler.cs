using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Ingredients.ReferenceIngredients.Commands
{
    public class
        EditReferenceIngredientCommandHandler : IRequestHandler<EditReferenceIngredientCommand, EntityCreatedResult>
    {
        private readonly ReferenceIngredientService _referenceIngredientService;

        public EditReferenceIngredientCommandHandler(ReferenceIngredientService referenceIngredientService)
        {
            _referenceIngredientService = referenceIngredientService;
        }

        public async Task<EntityCreatedResult> Handle(EditReferenceIngredientCommand command,
            CancellationToken cancellationToken)
        {
            var eventModel = command.Adapt<ReferenceIngredient>();
            var result = await _referenceIngredientService.Update(eventModel);
            return new EntityCreatedResult(result.Id);
        }
    }
}