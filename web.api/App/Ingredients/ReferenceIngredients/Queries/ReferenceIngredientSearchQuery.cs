using Common;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Ingredients.ReferenceIngredients.Queries
{
    public class ReferenceIngredientSearchQuery : BaseSearchQuery<ReferenceIngredientResponse>,
        IRequest<PageView<ReferenceIngredientResponse>>
    {
        public string Word { get; set; }
    }
}