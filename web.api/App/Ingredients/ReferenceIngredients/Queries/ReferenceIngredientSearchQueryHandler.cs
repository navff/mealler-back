using System.Threading;
using System.Threading.Tasks;
using Common;
using Mapster;
using MediatR;

namespace web.api.App.Ingredients.ReferenceIngredients.Queries
{
    public class ReferenceIngredientSearchQueryHandler : IRequestHandler<ReferenceIngredientSearchQuery,
        PageView<ReferenceIngredientResponse>>
    {
        private readonly ReferenceIngredientService _referenceIngredientService;

        public ReferenceIngredientSearchQueryHandler(ReferenceIngredientService referenceIngredientService)
        {
            _referenceIngredientService = referenceIngredientService;
        }

        public async Task<PageView<ReferenceIngredientResponse>> Handle(ReferenceIngredientSearchQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _referenceIngredientService.Search(request);
            return result.Adapt<PageView<ReferenceIngredientResponse>>();
        }
    }
}