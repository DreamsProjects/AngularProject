using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblPizza = new HashSet<TblPizza>();
            TblPost = new HashSet<TblPost>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<TblPizza> TblPizza { get; set; }
        public ICollection<TblPost> TblPost { get; set; }
    }
}
