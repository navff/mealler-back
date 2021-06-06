using System;
using MediatR;
using web.api.App.Common;

namespace web.api.App.Ingredients.ReferenceIngredients.Commands
{
    public class EditReferenceIngredientRequest
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Unit { get; set; }

        public Decimal PackPrice { get; set; }

        public Decimal PackAmount { get; set; }

        public int TeamId { get; set; }
    }

    public class EditReferenceIngredientCommand : EditReferenceIngredientRequest, IRequest<EntityCreatedResult>
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}