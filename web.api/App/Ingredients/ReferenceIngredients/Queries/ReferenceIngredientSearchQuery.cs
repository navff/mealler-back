using Common;
using MediatR;
using web.api.App.Common;
using web.api.App.Events;

namespace web.api.App.Ingredients.ReferenceIngredients.Queries
{
    public class ReferenceIngredientSearchQuery : BaseSearchQuery<EventResponse>,
        IRequest<PageView<ReferenceIngredientResponse>>
    {
        public string Word { get; set; }
    }
}