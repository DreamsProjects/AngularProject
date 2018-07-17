using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularProject.Models;
using AngularProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AngularProject.Controllers
{
    [Route("api/[Controller]")]
    public class FoodController : BaseController
    {
        /********** Get functions **********/

        [HttpGet("/api/GetPizzas")]
        public List<TblPizza> Index()
        {
            var result = _ctx.TblPizza.ToList();
            return result;
        }

        [HttpGet("/api/PizzaInfo")]
        public PizzaViewModel Pizzas()
        {
            var result = _ctx.TblPizza.ToList();

            var category = _ctx.TblCategory.FirstOrDefault();

            var orders = new PizzaViewModel()
            {

            };

            return orders;
        }

        [HttpGet("/id")]
        public TblPizza GetPizzaById(int id)
        {
            var result = _ctx.TblPizza.FirstOrDefault(x => x.PizzaId == id);
            return result;
        }

        [HttpGet("/getDecimal")]
        public JsonResult GetPriceById(int id)
        {
            var result = _ctx.TblPizza.FirstOrDefault(x => x.PizzaId == id);
            return Json(new { price = result.Price });
        }

        [HttpGet("/getIngredients")]
        public List<TblIngredient> GetIngredients() //Fixa namn på p och i
        {
            var result = _ctx.TblIngredient.ToList();

            return result;
        }

        [HttpGet("/category")]
        public List<TblCategory> GetCategory() //Fixa namn på p och i
        {
            var result = _ctx.TblCategory.Where(x => x.CategoryId < 7 || x.CategoryId == 13).ToList();

            return result;
        }

        [HttpGet("/connections")]
        public List<TblPizzaIngredients> PizzaIngredients()
        {
            var pizza = _ctx.TblPizzaIngredients.ToList();

            return pizza;
        }

        [HttpGet("/getSize")]
        public List<TblCategory> GetSize() //Fixa namn på p och i
        {
            var result = _ctx.TblCategory.Where(x => x.CategoryId == 7 || x.CategoryId == 8 || x.CategoryId == 11 || x.CategoryId == 12).ToList();

            return result;
        }
        /********** Post functions **********/

        [HttpPost("/removePizza")]
        public JsonResult RemovePizza([FromBody]TblPizza pizza)
        {
            if (ModelState.IsValid)
            {
                var delete = _ctx.TblPizza.FirstOrDefault(x => x.PizzaId == pizza.PizzaId);

                _ctx.TblPizza.Remove(delete);
                _ctx.SaveChanges();
                return Json(new { result = "Success" });
            }

            return Json(new { result = "Fail" });
        }

        [HttpPost("/updatePizza")]
        public JsonResult UpdatePizza([FromBody] TblPizza pizza)
        {
            if (ModelState.IsValid)
            {
                var update = _ctx.TblPizza.FirstOrDefault(x => x.PizzaId == pizza.PizzaId);
                update.Price = pizza.Price;
                update.Name = pizza.Name;
                update.CategoryId = pizza.CategoryId;

                _ctx.TblPizza.Update(update);
                _ctx.SaveChanges();

                return Json(new { result = "Success" });
            }

            return Json(new { result = "Fail" });
        }

        [HttpPost("/makeIngredients")]
        public JsonResult MakeIngredients([FromBody]TblIngredient ingredient)
        {
            var createIngridient = _ctx.TblIngredient.FirstOrDefault(x => x.Name == ingredient.Name);

            if (createIngridient == null)
            {
                int? newId = _ctx.TblIngredient.Max(x => (int?)x.IngredientId);

                if (newId == null)
                {
                    newId = 1;
                }

                else
                {
                    newId++;
                }

                if (ModelState.IsValid)
                {
                    var createIngredient = new TblIngredient
                    {
                        IngredientId = (int)newId,
                        Name = ingredient.Name,
                        Price = ingredient.Price,
                    };

                    _ctx.TblIngredient.Add(createIngredient);
                    _ctx.SaveChanges();

                    return Json(new { status = "Successful" });
                }
                return Json(new { status = "Fail" });
            }
            return Json(new { status = "Already existing" });
        }

        [HttpPost("/createPizza")]
        public JsonResult CreatePizza([FromBody] TblPizza pizza)
        {
            var createIngridient = _ctx.TblPizza.FirstOrDefault(x => x.Name == pizza.Name);

            if (createIngridient == null)
            {
                int? newId = _ctx.TblPizza.Max(x => (int?)x.PizzaId);

                if (newId == null)
                {
                    newId = 1;
                }

                else
                {
                    newId++;
                }

                if (pizza.Price == 0)
                {
                    pizza.Price = 75;
                }

                if (ModelState.IsValid)
                {
                    var createPizza = new TblPizza
                    {
                        PizzaId = (int)newId,
                        Name = pizza.Name,
                        Price = pizza.Price,
                        CategoryId = pizza.CategoryId
                    };

                    _ctx.TblPizza.Add(createPizza);
                    _ctx.SaveChanges();

                    return Json(new { status = "Successful" });
                }
                return Json(new { status = "Fail" });
            }
            return Json(new { status = "Already existing" });

        }

        [HttpPost("/makePizzas")]
        public JsonResult MakePizza([FromBody]TblPizzaIngredients pizza)
        {
            var tblpizza = _ctx.TblPizza.FirstOrDefault(x => x.PizzaId == pizza.PizzaId);
            var product = _ctx.TblPizzaIngredients.FirstOrDefault(x => x.PizzaId == pizza.PizzaId);
            var ingredient = _ctx.TblIngredient.FirstOrDefault(x => x.IngredientId == pizza.IngredientId);

            int? newId = _ctx.TblPizzaIngredients.Max(x => (int?)x.PizzaIngredientId);

            if (newId == null)
            {
                newId = 1;
            }

            else
            {
                newId++;
            }

            if (product == null && pizza.IngredientName != null)
            {
                var pizzas = new TblPizzaIngredients()
                {
                    PizzaIngredientId = (int)newId,
                    IngredientId = pizza.IngredientId,
                    PizzaId = pizza.PizzaId,
                    PizzaName = tblpizza.Name,
                    IngredientName = ingredient.Name
                };

                _ctx.TblPizzaIngredients.Add(pizzas);
                _ctx.SaveChanges();

                return Json(new { response = "Success" });
            }

            else
            {
                if (product.IngredientId != pizza.IngredientId) //Om pizzans ingredients inte finns på pizzan
                {
                    var pizzas = new TblPizzaIngredients()
                    {
                        PizzaIngredientId = (int)newId,
                        IngredientId = pizza.IngredientId,
                        PizzaId = pizza.PizzaId
                    };

                    _ctx.TblPizzaIngredients.Add(pizzas);
                    _ctx.SaveChanges();

                    return Json(new { response = "Success" });
                }
            }

            return Json(new { response = "Already existing" });
        }

        /*Denna metoden ska användas senare för att lägga till flera ingredienser på samma pizza*/
        /* [HttpPost("/makePizzas")]
         public JsonResult MakePizza(List<TblPizzaIngredients> pizza)
         {
             var product = _ctx.TblPizzaIngredients.ToList();

             foreach (var item in pizza)
             {
                 foreach (var products in product)
                 {
                     if (products.IngredientId != item.IngredientId) //Om pizzans ingredients inte finns på pizzan
                     {
                         var pizzas = new TblPizzaIngredients()
                         {
                             IngredientId = item.IngredientId,
                             PizzaId = item.PizzaId
                         };

                         _ctx.TblPizzaIngredients.Add(pizzas);
                         _ctx.SaveChanges();
                     }
                 }
             };

             return Json(new { response = "Success" });
         }
         */
    }
}