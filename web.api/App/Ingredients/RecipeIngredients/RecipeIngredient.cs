using System;
using System.ComponentModel.DataAnnotations.Schema;
using web.api.App.Common;
using web.api.App.Recipes;

namespace web.api.App.Ingredients.RecipeIngredients
{
    public class RecipeIngredient
    {
        public int Id { set; get; }

        [ForeignKey("Recipe")] public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public Double Amount { get; set; }

        public Unit Unit { get; set; }
    }
}