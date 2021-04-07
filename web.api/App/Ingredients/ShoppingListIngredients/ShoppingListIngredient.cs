using System;
using System.ComponentModel.DataAnnotations.Schema;
using web.api.App.Common;
using web.api.App.Users;

namespace web.api.App.Ingredients.ShoppingListIngredients
{
    public class ShoppingListIngredient
    {
        public int Id { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Cost { get; set; }
        public bool Bought { get; set; }
        public Decimal PackPrice { get; set; }
        public Decimal PackAmount { get; set; }
        public string Name { get; set; }
        public Unit Unit { get; set; }
        public Color Color { get; set; }

        [ForeignKey("User")] public int UserId { get; set; }

        public virtual User User { get; set; }
    }

    public enum Color
    {
        Green = 1,
        Blue = 2,
        Peach = 3,
        Yellow = 4,
        White = 5
    }
}