using web.api.App.Common;
using web.api.App.Events;

namespace web.api.App.Ingredients.ReferenceIngredients.Queries
{
    public class ReferenceIngredientSearchQuery : BaseSearchQuery<EventResponse>
    {
        public string Word { get; set; }
    }
}