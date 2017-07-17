using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClimateStore.Domain.Abstract;
using ClimateStore.Domain.Entities;
using ClimateStore.WebUI.Models;

namespace ClimateStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)//added Cart cart to bind
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl

            }
                );
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl)//added Cart cart to bind
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                //GetCart().AddItem(product, 1);
               cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)//added Cart cart to bind
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if(cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart; 
        //    }
        //    return cart;
        //}
        /*
         удалили метод GetCart и добавили параметр Cart cart в каждый метод действия. Когда MVC
  Framework получает запрос, который требует, скажем, вызвать метод AddToCart, она будет сначала
  смотреть на параметры для метода действия. Она рассмотрит список доступных механизмов
  связывания и попытается найти тот, который сможет создать экземпляры каждого типа параметра.
  Наш пользовательский механизм связывания должен будет создать объект Cart, что он и сделает,
  используя состояние сеанса. Между обращениями к нашему механизму связывания и механизму по
  умолчанию MVC Framework может создать набор параметров, которые необходимы для вызова
  метода действия, позволяя нам реорганизовать контроллер так, чтобы в нем не осталось сведений о
  том, как создаются объекты Cart при получении запросов.
           */

            //Partial for cart
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        //For "Checkout now" Button
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}