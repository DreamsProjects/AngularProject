using System;
using System.Collections.Generic;

namespace AngularProject.Models
{
    public partial class TblPerson
    {
        public TblPerson()
        {
            TblHighscore = new HashSet<TblHighscore>();
            TblPost = new HashSet<TblPost>();
            TblOrder = new HashSet<TblOrder>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Telephone { get; set; }
        public DateTime? LastLoggedIn { get; set; }

        public ICollection<TblHighscore> TblHighscore { get; set; }
        public ICollection<TblPost> TblPost { get; set; }
        public ICollection<TblOrder> TblOrder { get; set; }
    }
}
