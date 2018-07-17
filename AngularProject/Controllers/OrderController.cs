using AngularProject.Models;
using AngularProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularProject.Controllers
{
    public class OrderController : BaseController
    {
        public List<TblPizza> FoodList;

        [HttpGet("/order/delivered")]
        public List<TblOrder> DeliveredToday()
        {
            var delivered = _ctx.TblOrder.Where(x => x.Delivered == true && x.Date.Date == DateTime.Now.Date).ToList();
            return delivered;
        }

        [HttpGet("/order/notDelivered")]
        public List<TblOrder> NotDeliveredOrdersToday()
        {
            var moneyThisDay = _ctx.TblOrder.Where(x => x.Date.Date == DateTime.Now.Date && x.Delivered == false).ToList();

            return moneyThisDay;
        }

        [HttpGet("/order/today")]
        public decimal MoneyToday()
        {
            decimal thisDay = 0;
            var moneyThisDay = _ctx.TblOrderInfo.Where(x => x.Ordered.Value.Date == DateTime.Now.Date).ToList();

            foreach (var money in moneyThisDay)
            {
                thisDay += money.AmountPerId;
            }

            return thisDay;
        }

        [HttpGet("/order/year")]
        public decimal MoneyYear()
        {
            decimal thisYear = 0;
            var moneyThisYear = _ctx.TblOrderInfo.Where(x => x.Ordered.Value.Year == DateTime.Now.Year).ToList();

            foreach (var money in moneyThisYear)
            {
                thisYear += money.AmountPerId;
            }

            return thisYear;
        }

        [HttpGet("/orderDetails")]
        public OrderViewModel NotDeliveredToday()
        {
            var moneyThisDay = _ctx.TblOrderInfo.Where(x => x.Ordered.Value.Date == DateTime.Now.Date).ToList();
            var moneyThisYear = _ctx.TblOrderInfo.Where(x => x.Ordered.Value.Year == DateTime.Now.Year).ToList();

            var cookie = Request.Cookies["User"];

            var person = Convert.ToInt32(cookie);

            decimal thisDay = 0;
            decimal thisYear = 0;

            foreach (var money in moneyThisDay)
            {
                thisDay += money.AmountPerId;
            }

            foreach (var money in moneyThisYear)
            {
                thisYear += money.AmountPerId;
            }

            var notDelivered = _ctx.TblOrder.Where(x => x.Delivered == false || x.Delivered == null).ToList();
            var delivered = _ctx.TblOrder.Where(x => x.Delivered == true).ToList();

            var orders = new OrderViewModel()
            {
                ThisDay = thisDay,
                ThisYear = thisYear,
                NotDelivered = notDelivered,
                Delivered = delivered,
                AllOrdersToday = moneyThisDay
            };

            return orders;
        }

        [HttpGet("/read")]
        public List<TblPizza> ReadCart()
        {
            var value = (HttpContext.Session.GetString("Varukorg"));

            if (value != null)
            {
                FoodList = JsonConvert.DeserializeObject<List<TblPizza>>(value);
            }

            return FoodList;
        }

        [HttpGet("/count")]
        public decimal Count()
        {
            var value = (HttpContext.Session.GetString("Varukorg"));

            decimal values = 0;

            if (value != null)
            {
                FoodList = JsonConvert.DeserializeObject<List<TblPizza>>(value);

                foreach (var items in FoodList)
                {
                    values += items.Price;
                }
            }
            return values;
        }

        [HttpGet("/order/userOrders")]
        public List<TblOrder> UserOrders()
        {
            var cookie = Request.Cookies["User"];

            var person = Convert.ToInt32(cookie);

            var personOrders = _ctx.TblOrder.Where(x => x.PersonID == person).ToList();
            return personOrders;
        }

        [HttpPost("/orderInfo")]
        public List<TblOrderInfo> GetInfo([FromBody] TblOrder order)
        {
            var getOrderInfo = _ctx.TblOrderInfo.Where(x => x.OrderId == order.OrderId).ToList();

            return getOrderInfo;
        }

        [HttpPost("/getOrderClient")]
        public TblPerson GetPerson([FromBody] TblOrder order)
        {
            var findUser = _ctx.TblOrder.FirstOrDefault(x => x.OrderId == order.OrderId);

            var person = _ctx.TblPerson.FirstOrDefault(x => x.PersonId == findUser.PersonID);

            return person;
        }

        [HttpPost("/markAsDelivered")]
        public JsonResult IsDelivered([FromBody] TblOrder order)
        {
            if (order != null)
            {
                var findOrder = _ctx.TblOrder.FirstOrDefault(x => x.OrderId == order.OrderId);

                findOrder.Delivered = true;

                _ctx.TblOrder.Update(findOrder);
                _ctx.SaveChanges();

                return Json(new { result = "Success" });
            }

            return Json(new { result = "Fail" });
        }

        [HttpPost("/sendOrder")]
        public JsonResult SendOrder([FromBody] TblOrder order)
        {
            var value = (HttpContext.Session.GetString("Varukorg"));

            var cookie = Request.Cookies["User"];

            var person = Convert.ToInt32(cookie);

            if (value != null)
            {
                FoodList = JsonConvert.DeserializeObject<List<TblPizza>>(value);

                int? newOrderId = _ctx.TblOrder.Max(x => (int?)x.OrderId);
                newOrderId++;
                var addToOrder = new TblOrder
                {
                    OrderId = newOrderId ?? 1,
                    Delivered = false,
                    Date = DateTime.Now,
                    TotalAmount = order.TotalAmount,
                    CardOrCash = order.CardOrCash,
                    PersonID = person
                };

                _ctx.TblOrder.Add(addToOrder);
                _ctx.SaveChanges();

                foreach (var items in FoodList)
                {
                    int? newOrderInfoId = _ctx.TblOrderInfo.Max(x => (int?)x.OrderInfoId);
                    newOrderInfoId++;
                    var addToOrderInfo = new TblOrderInfo
                    {
                        OrderId = newOrderId ?? 1,
                        OrderInfoId = (int)newOrderInfoId,
                        PizzaId = items.PizzaId,
                        AmountPerId = items.Price,
                        CategoryId = items.CategoryId,
                        Ordered = DateTime.Now,
                        Name = items.Name
                    };
                    _ctx.TblOrderInfo.Add(addToOrderInfo);
                    _ctx.SaveChanges();
                }

                foreach (var item in FoodList) //Raderar efter köp
                {
                    FoodList.Remove(item);
                }

                return Json(new { result = "Success" });
            }

            return Json(new { result = "Fail" });
        }

        [HttpPost("/cart")]
        public JsonResult AddToCart([FromBody]TblPizza pizza)
        {
            var foodOrder = _ctx.TblPizza.FirstOrDefault(x => x.PizzaId == pizza.PizzaId);
            foodOrder.Price = pizza.Price;
            foodOrder.CategoryId = pizza.CategoryId;

            List<TblPizza> FoodList;

            if (HttpContext.Session.GetString("Varukorg") == null)
            {
                FoodList = new List<TblPizza>();
            }

            else
            {
                var value = (HttpContext.Session.GetString("Varukorg"));
                FoodList = JsonConvert.DeserializeObject<List<TblPizza>>(value);
            }

            FoodList.Add(foodOrder);

            var tempo = JsonConvert.SerializeObject(FoodList);
            HttpContext.Session.SetString("Varukorg", tempo);

            return Json(new { result = "Success" });
        }

        [HttpPost("/RemoveFromcart")]
        public JsonResult RemoveFromCart([FromBody] PizzaViewModel pizza)
        {
            var value = (HttpContext.Session.GetString("Varukorg"));

            FoodList = JsonConvert.DeserializeObject<List<TblPizza>>(value);

            var item = FoodList.FirstOrDefault(x => x.PizzaId == pizza.PizzaId && x.CategoryId == pizza.CategoryId);
            FoodList.Remove(item);

            var tempo = JsonConvert.SerializeObject(FoodList);
            HttpContext.Session.SetString("Varukorg", tempo);


            return Json(new { result = "Success" });
        }
    }
}