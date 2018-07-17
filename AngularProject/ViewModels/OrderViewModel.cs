using AngularProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.ViewModels
{
    public class OrderViewModel
    {
        public decimal ThisDay { get; set; }
        public decimal ThisYear { get; set; }
        public int OrderId { get; set; }
        public string PizzaName { get; set; }
        public string IngredientName { get; set; }

        public List<TblOrderInfo> User { get; set; }
        public List<TblOrder> NotDelivered{ get; set; }
        public List<TblOrder> Delivered { get; set; }
        public List<TblOrderInfo> AllOrdersToday { get; set; }
    }
}