using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace web.api.App.Ingredients.ReferenceIngredients.Commands
{
    public class DeleteReferenceIngredientRequest
    {
        public int Id { get; set; }
    }

    public class DeleteReferenceIngredientCommand : DeleteReferenceIngredientRequest, IRequest<EmptyResult>
    {
        public string Username { get; set; }
    }
}