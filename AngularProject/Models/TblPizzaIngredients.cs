using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblPizzaIngredients
    {
        public int PizzaIngredientId { get; set; }
        public int? IngredientId { get; set; }
        public int? PizzaId { get; set; }
        public string PizzaName { get; set; }
        public string IngredientName { get; set; }

        public TblIngredient Ingredient { get; set; }
        public TblPizza Pizza { get; set; }
    }
}
