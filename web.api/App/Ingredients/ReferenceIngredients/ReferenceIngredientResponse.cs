using System;

namespace web.api.App.Ingredients.ReferenceIngredients
{
    public class ReferenceIngredientResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Unit { get; set; }

        public Decimal PackPrice { get; set; }

        public Decimal PackAmount { get; set; }

        public int TeamId { get; set; }
    }
}