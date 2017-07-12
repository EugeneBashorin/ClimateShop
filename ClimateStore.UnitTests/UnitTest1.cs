using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClimateStore.Domain.Entities;
using ClimateStore.Domain.Abstract;
using ClimateStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClimateStore.WebUI.Models;
using ClimateStore.WebUI.HtmlHelpers;

namespace ClimateStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate() //тест: нумерация страниц
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //Act
            //IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;
            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()//тест: создание ссылок на страницы
        {
            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper myHelper = null;

            // Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //ACT
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            //ASSERT
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
                                                + @"<a class=""selected"" href=""Page2"">2</a>"
                                                + @"<a href=""Page3"">3</a>");
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
               new Product {ProductID = 1, Name = "P1"},
               new Product {ProductID = 2, Name = "P2"},
               new Product {ProductID = 3, Name = "P3"},
               new Product {ProductID = 4, Name = "P4"},
               new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable());

            //Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //ACT
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;
            //ASSERT
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
/*
 вызываем свойство Model в результате, чтобы получить последовательность IEnumerable<Product>,
которую мы генерировали в методе List. Затем мы можем проверить, те ли это данные, которые мы
хотим получить. В этом случае мы преобразовали последовательность в массив, и проверили длину и
значение отдельных объектов.
     */
