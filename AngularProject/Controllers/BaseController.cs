using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AngularProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularProject.Controllers
{
    public class BaseController : Controller
    {
        public TblPerson _user;

        private Summer_projectContext _context;

        public IActionResult ViewCookies(int personId)
        {
            _user = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == personId);
            CookieOptions options = new CookieOptions();
            options.HttpOnly = true;
            options.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("User", personId.ToString(), options);

            return View();
        }

        protected Summer_projectContext _ctx
        {
            get
            {
                if (_context == null)
                {
                    _context = new Summer_projectContext();
                }
                return _context;
            }
        }

    }
}