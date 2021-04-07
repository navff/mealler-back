using System;
using System.ComponentModel.DataAnnotations;
using web.api.App.Common;

namespace web.api.App.Ingredients.ReferenceIngredients
{
    public class ReferenceIngredient
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public Unit Unit { get; set; }

        public Decimal PackPrice { get; set; }

        public Decimal PackAmount { get; set; }

        public int TeamId { get; set; }
    }
}