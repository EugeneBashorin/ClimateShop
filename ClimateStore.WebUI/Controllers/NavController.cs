using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClimateStore.Domain.Abstract;

namespace ClimateStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu()
        {
            IEnumerable<string> category = repository.Products
                .Select(x => x.Category)
                .Distinct()//Выборка по уникальным значениям
                .OrderBy(x => x);
            return PartialView(category);
        }
        // GET: Nav
       // public string Menu()
       // {
       //     return "Hello from NavController";
       // }
    }
}
/*
конструктор, принимает реализацию IProductRepository как аргумент - после создания экземпляра контроллера ее предоставит Ninject, используя привязки.
***
Метод действия Menu, использует запрос LINQ, чтобы получить список категорий из хранилища и передать их в представление.
Так как в контроллере мы работаем с частичным представлением, мы вызываем метод PartialView, результатом является объект PartialViewResult.
 */
