using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClimateStore.Domain.Abstract;
using System.Web.Mvc;
using ClimateStore.Domain.Entities;

namespace ClimateStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product,HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)//механизм связывания провел валидацию представленных пользователем данных?
            {
                if(image !=null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }

        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if(deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}