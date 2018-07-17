using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using AngularProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularProject.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        public IActionResult Competition() //Shotgun med javascript, läser från ajax och skickar in det i en postmetod när man är klar och ska submitta sina poäng
        {
            return View();
        }

        public IActionResult Delete() //ViewModel
        {
            return View();
        }

        public IActionResult Edit() //ViewModel
        {
            return View();
        }     
    }
}
