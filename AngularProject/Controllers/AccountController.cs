using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AngularProject.Handlers;
using AngularProject.Models;
using AngularProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AngularProject.Handlers.SessionHandler;

namespace AngularProject.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet("/api/GetUser")]
        public TblPerson User()
        {
            var cookie = Request.Cookies["User"];

            var person = Convert.ToInt32(cookie);


            if (cookie != null)
            {
                _user = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == person);
            }

            if (person == 0)
            {
                person = 2;
                _user = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == 2);
            }

            var isNotNull = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == person);


            return isNotNull;
        }

        [HttpPost("/api/login")]
        public JsonResult Login([FromBody] TblPerson person)
        {
            switch (SessionHandler.Login(person.Email, person.Pass))
            {
                case ReturnValue.Successful:

                    var number = _ctx.TblPerson.FirstOrDefault(x => x.Email == person.Email);
                    _user = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == number.PersonId);
                    ViewCookies(_user.PersonId);

                    return Json(new { result = "Success", user = _user.PersonId }); //användarens inloggning sparas i en cookie


                case ReturnValue.WrongUserOrPassword:

                    if (_user == null)
                    {
                        _user = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == 2);
                    }

                    return Json(new { result = "Fail", user = _user.PersonId });
            }

            return Json(new { result = "Fail" });
        }

        [HttpGet("/LogOut")]
        public JsonResult Logout()
        {
            var reset = 0;
            _user = null;
            ViewCookies(reset); //sätter _user till noll
            return Json(new { result = "Successed", user = _user });
        }

        [HttpPost("/api/createUser")]
        public JsonResult CreateUser([FromBody] TblPerson person)
        {
            var findEmail = _ctx.TblPerson.FirstOrDefault(x => x.Email == person.Email);

            if (findEmail == null)
            {
                int? newId = _ctx.TblPerson.Max(x => (int?)x.PersonId);

                if (newId == null) newId = 1;

                else newId++;

                var createUser = new TblPerson
                {
                    PersonId = (int?)newId ?? 1,
                    Email = person.Email,
                    Pass = person.Pass,
                    Address = person.Address,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    LastLoggedIn = null,
                    Telephone = person.Telephone
                };

                _ctx.TblPerson.Add(createUser);
                _ctx.SaveChanges();

                ViewCookies(createUser.PersonId);
                _user = createUser;


                return Json(new { result = "Successed" });
            }

            return Json(new { result = "Fail" });
        }

        [HttpPost("/changeInfo")]
        public JsonResult ChangeInfo([FromBody] TblPerson person)
        {
            var cookie = Request.Cookies["User"];

            var personId = Convert.ToInt32(cookie);

            if (ModelState.IsValid)
            {
                var findPerson = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == personId);

                findPerson.FirstName = person.FirstName;
                findPerson.LastName = person.LastName;
                findPerson.Address = person.Address;
                findPerson.Email = person.Email;
                findPerson.Telephone = person.Telephone;
                findPerson.Pass = person.Pass;

                _ctx.TblPerson.Update(findPerson);
                _ctx.SaveChanges();

                return Json(new { result = "Successed" });
            }

            return Json(new { result = "Fail" });
        }

        [HttpPost("/winner")]
        public JsonResult Winner([FromBody] TblHighscore points)
        {
            var cookie = Request.Cookies["User"];

            var person = Convert.ToInt32(cookie);

            var findPerson = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == person);

            int? newId = _ctx.TblHighscore.Max(x => (int?)x.HighScoreId);

            if (newId == null) newId = 1;

            else newId++;

            var newHighScore = new TblHighscore
            {
                HighScoreId = (int?)newId ?? 1,
                PersonId = findPerson.PersonId,
                Points = points.Points
            };

            _ctx.TblHighscore.Add(newHighScore);
            _ctx.SaveChanges();


            return Json(new { result = "Successed" });
        }
    }
}