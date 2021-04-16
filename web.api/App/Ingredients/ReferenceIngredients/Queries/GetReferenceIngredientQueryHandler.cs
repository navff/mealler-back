using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;

namespace web.api.App.Ingredients.ReferenceIngredients.Queries
{
    public class
        GetReferenceIngredientQueryHandler : IRequestHandler<GetReferenceIngredientQuery, ReferenceIngredientResponse>
    {
        private readonly ReferenceIngredientService _referenceIngredientService;

        public GetReferenceIngredientQueryHandler(ReferenceIngredientService referenceIngredientService)
        {
            _referenceIngredientService = referenceIngredientService;
        }

        public async Task<ReferenceIngredientResponse> Handle(GetReferenceIngredientQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _referenceIngredientService.Get(request.Id);
            return result.Adapt<ReferenceIngredientResponse>();
        }
    }
}