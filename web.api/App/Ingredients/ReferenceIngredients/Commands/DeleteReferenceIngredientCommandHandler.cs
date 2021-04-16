using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace web.api.App.Ingredients.ReferenceIngredients.Commands
{
    public class
        DeleteReferenceIngredientCommandHandler : IRequestHandler<DeleteReferenceIngredientCommand, EmptyResult>
    {
        private readonly ReferenceIngredientService _referenceIngredientService;

        public DeleteReferenceIngredientCommandHandler(ReferenceIngredientService referenceIngredientService)
        {
            _referenceIngredientService = referenceIngredientService;
        }

        public async Task<EmptyResult> Handle(DeleteReferenceIngredientCommand command,
            CancellationToken cancellationToken)
        {
            await _referenceIngredientService.CheckRights(command.Id, command.Username);
            await _referenceIngredientService.Delete(command.Id);
            return new EmptyResult();
        }
    }
}