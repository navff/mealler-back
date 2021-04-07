using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using web.api.App.Ingredients.RecipeIngredients;
using web.api.App.Recipes;

namespace web.api.App.Meals
{
    /*
     * {
        id: 1,
        recipeName: 'Джаганнатха пури чанаки дал',
        recipeId: 1,
        portions: 120,
        laborCosts: 1.5,
        cookingTime: 4.5,
        cost: 1350,
        ingredients: ingredients
      }
     */
    public class Meal
    {
        public int Id { get; set; }


        [ForeignKey("Recipe")] public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        /// <summary>
        /// Количество порций этого блюда на конкретное мероприятие
        /// </summary>
        public int Portions { get; set; }

        /// <summary>
        /// Трудозатраты
        /// </summary>
        public Decimal LaborCosts { get; set; }

        /// <summary>
        /// Время приготовления блюда на конкретное мероприятие
        /// </summary>
        public Decimal CookingTime { get; set; }

        /// <summary>
        /// Общая стоимость блюда
        /// </summary>
        public Decimal Cost { get; set; }

        public ICollection<RecipeIngredient> Ingredients => Recipe.Ingredients;
    }
}