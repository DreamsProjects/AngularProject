using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblHighscore
    {
        public int HighScoreId { get; set; }
        public int? PersonId { get; set; }
        public int Points { get; set; }

        public TblPerson Person { get; set; }
    }
}
