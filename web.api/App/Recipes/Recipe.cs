using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using web.api.App.Ingredients.RecipeIngredients;

namespace web.api.App.Recipes
{
    public class Recipe
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }
    }
}