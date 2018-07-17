using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderInfo = new HashSet<TblOrderInfo>();
        }

        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public bool? Delivered { get; set; }
        public string CardOrCash { get; set; }
        public int PersonID { get; set; }

        public TblPerson Person { get; set; }
        public ICollection<TblOrderInfo> TblOrderInfo { get; set; }
    }
}
