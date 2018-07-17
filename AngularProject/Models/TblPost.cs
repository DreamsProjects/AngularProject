using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblPost
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int PersonId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; }

        public TblCategory Category { get; set; }
        public TblPerson Person { get; set; }
    }
}
