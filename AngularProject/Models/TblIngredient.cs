using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblIngredient
    {
        public TblIngredient()
        {
            TblPizzaIngredients = new HashSet<TblPizzaIngredients>();
        }

        public int IngredientId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public ICollection<TblPizzaIngredients> TblPizzaIngredients { get; set; }
    }
}
