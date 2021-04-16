using System;
using MediatR;
using web.api.App.Common;
using Unit = web.api.App.Common.Unit;

namespace web.api.App.Ingredients.ReferenceIngredients.Commands
{
    public class AddReferenceIngredientRequest
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public Unit Unit { get; set; }

        public Decimal PackPrice { get; set; }

        public Decimal PackAmount { get; set; }

        public int TeamId { get; set; }
    }

    public class AddReferenceIngredientCommand : AddReferenceIngredientRequest, IRequest<EntityCreatedResult>
    {
        public string Username { get; set; }
    }
}