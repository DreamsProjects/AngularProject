using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblPizza
    {
        public TblPizza()
        {
            TblOrderInfo = new HashSet<TblOrderInfo>();
            TblPizzaIngredients = new HashSet<TblPizzaIngredients>();
        }

        public int PizzaId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Photos { get; set; }

        public TblCategory Category { get; set; }
        public ICollection<TblOrderInfo> TblOrderInfo { get; set; }
        public ICollection<TblPizzaIngredients> TblPizzaIngredients { get; set; }
    }
}
