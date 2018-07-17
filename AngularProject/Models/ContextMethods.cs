using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Models
{
    public class ContextMethods
    {

        public static TblPerson GetUserFromLogin(string userName, string password, string SHApassword, string SHApassword2) //SessionHandler
        {
            var _ctx = new Summer_projectContext();

            var user = _ctx.TblPerson.FirstOrDefault(u => u.Email == userName && (u.Pass == password || u.Pass == SHApassword || u.Pass == SHApassword2));

            if (user != null)
            {
                ContextMethods.UpdateUserPassword(user.PersonId, SHApassword);
            }

            return user;
        }

        public static void UpdateUserPassword(int userId, string pass)
        {
            var _ctx = new Summer_projectContext();

            var user = _ctx.TblPerson.FirstOrDefault(u => u.PersonId == userId);
            user.Pass = pass;
            _ctx.TblPerson.Update(user);
            _ctx.SaveChanges();
        }
    }
}
