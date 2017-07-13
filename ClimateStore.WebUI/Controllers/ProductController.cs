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

        public ViewResult List(string category,int page = 1)//add category
        {
            //объект ProductsListViewModel в качестве данных модели в представлени
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)// если category не содержит null, будут выбраны только те объекты Product, которые соответствуют свойству Category
                .OrderBy(p => p.ProductID)            //упорядочиваем их по первичному ключу
                .Skip((page - 1) * PageSize)         //пропускаем товары которые идут до начала нашей страницы
                .Take(PageSize),                    //берем количество товаров, указанное в поле PageSize
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    
                    //Если категория выбрана, возвращаем количество товаров в этой категории, если нет, возвращаем общее количество товаров.
                    TotalItems = category == null ?
                    repository.Products.Count() :
                    repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category //установили значение свойства CurrentCategory, добавленного в класс ProductsListViewModel
            };
            return View(model);
            //return View(repository.Products         //получаемобъекты Product из хранилища
            //    .OrderBy(p=>p.ProductID)            //упорядочиваем их по первичному ключу
            //    .Skip((page - 1)* PageSize)         //пропускаем товары которые идут до начала нашей страницы
            //    .Take(PageSize));                   //берем количество товаров, указанное в поле PageSize

        }
    }
}