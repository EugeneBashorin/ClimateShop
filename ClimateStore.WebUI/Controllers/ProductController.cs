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
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;                //указывает, что мы хотим видеть четыре товара на странице
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(int page = 1)
        {
            //объект ProductsListViewModel в качестве данных модели в представлени
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                .OrderBy(p => p.ProductID)            //упорядочиваем их по первичному ключу
                .Skip((page - 1) * PageSize)         //пропускаем товары которые идут до начала нашей страницы
                .Take(PageSize),                    //берем количество товаров, указанное в поле PageSize
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            };
            return View(model);
            //return View(repository.Products         //получаемобъекты Product из хранилища
            //    .OrderBy(p=>p.ProductID)            //упорядочиваем их по первичному ключу
            //    .Skip((page - 1)* PageSize)         //пропускаем товары которые идут до начала нашей страницы
            //    .Take(PageSize));                   //берем количество товаров, указанное в поле PageSize

        }
    }
}