using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClimateStore.Domain.Entities;
using System.Linq;

namespace ClimateStore.UnitTests
{
    [TestClass]
   public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            // Arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] result = target.Lines.ToArray();
            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);
        }

        //Product уже есть в корзине, увеличиваем количество в соответствующем объекте CartLine и не создаваем новый
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            // Arrange - create a new cart
            Cart target = new Cart();
            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] result = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();
            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Quantity, 11);
            Assert.AreEqual(result[1].Quantity, 1);
        }

        //пользователи могут передумать и удалить товары из корзины
        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            //Act
            target.RemoveLine(p2);
            //Assert
            Assert.AreEqual(target.Lines.Where(p => p.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        //расчет общей стоимости товаров в корзине
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1",Price=100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            //// Arrange - create new cart
            Cart target = new Cart();
            // ACT
            target.AddItem(p1, 2);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.CalculateTotalValue();
            //ASSERT
            Assert.AreEqual(result, 550M);
        }

        //содержимое корзины удаляется, когда после очистки
        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            // Act - reset the cart
            target.Clear();
            //Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }
        }
}
