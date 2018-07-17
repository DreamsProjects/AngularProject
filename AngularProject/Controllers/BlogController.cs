using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularProject.Models;
//using AngularProject.Models;
using AngularProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AngularProject.Controllers
{
    [Route("api/[Controller]")]
    public class BlogController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/deletePost")]
        public JsonResult Delete([FromBody]TblPost post) //Ska ta in id och kunna ta bort sina egna inlägg
        {
            var cookie = Request.Cookies["User"];
            var userId = Convert.ToInt32(cookie);

            var postId = _ctx.TblPost.FirstOrDefault(x => x.PostId == post.PostId);

            if (userId == postId.PersonId)
            {
                _ctx.TblPost.Remove(postId);
                _ctx.SaveChanges();
                return Json(new { status = "Successfully Deleted" });
            }
            else
            {
                return Json(new { status = "You have not created this comment" });
            }
        }

        public JsonResult Edit(TblPost post) //Ska ta in id och kunna redigera sina egna inlägg
        {
            var postId = _ctx.TblPost.FirstOrDefault(x => x.PostId == post.PostId);

            if (post.PersonId == postId.PersonId)
            {
                postId.Text = post.Text;
                postId.Title = post.Title;
                _ctx.SaveChanges();

                return Json(new { status = "Successfully changed" });
            }
            else
            {
                return Json(new { status = "You have not created this comment" });
            }
        }

        [HttpPost("/addPost")]
        public JsonResult AddPost([FromBody]TblPost post) //TblPost post
        {
            var cookie = Request.Cookies["User"];
            var person = Convert.ToInt32(cookie);

            if (cookie == null || cookie == "0")
            {
                person = 2;
            }

            int? newId = _ctx.TblPost.Max(x => (int?)x.PostId);

            if(newId == null)
            {
                newId = 1;
            }

            else
            {
                newId++;
            }

            if (ModelState.IsValid)
            {
                var persons = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == person);
                var category = _ctx.TblCategory.FirstOrDefault(x => x.CategoryId == 10);

                var savePost = new TblPost
                {
                    Person = persons,
                    Category = category,
                    PostId = newId ?? 1,
                    PersonId = person,
                    Text = post.Text,
                    Title = post.Title,
                    CategoryId = 10,
                    FirstName = persons.FirstName,
                    Date = DateTime.Now
                };

                _ctx.TblPost.Add(savePost);
                _ctx.SaveChanges();

                return Json(new { status = "Successful" });
            }
            return Json(new { status = "Fail" });
        }

        [HttpGet("/findPost")]
        public List<TblPost> FindAllPost()
        {
            var findPost = _ctx.TblPost.OrderByDescending(x => x.PostId).ToList();

            return findPost;
        }

        [HttpGet("/getPostById")]
        public List<TblPost> FindAllPostById()
        {
            var cookie = Request.Cookies["User"];
            var id = Convert.ToInt32(cookie);


            var findUsersPost = _ctx.TblPost.Where(x => x.PersonId == id).ToList();

            return findUsersPost;
        }
    }
}