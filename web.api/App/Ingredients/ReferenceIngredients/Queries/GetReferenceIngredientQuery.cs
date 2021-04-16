using MediatR;

namespace web.api.App.Ingredients.ReferenceIngredients.Queries
{
    public class GetReferenceIngredientQuery : IRequest<ReferenceIngredientResponse>
    {
        public int Id { get; set; }
    }
}