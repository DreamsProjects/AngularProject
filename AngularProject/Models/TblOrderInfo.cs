using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblOrderInfo
    {
        public int OrderInfoId { get; set; }
        public int PizzaId { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal AmountPerId { get; set; }
        public int CategoryId { get; set; }
        public DateTime? Ordered { get; set; }
        public TblOrder Order { get; set; }
        public TblPizza Pizza { get; set; }
    }
}
