using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClimateStore.WebUI.Controllers;
using ClimateStore.WebUI.Infrastructure.Abstract;
using ClimateStore.WebUI.Models;
using System.Web.Mvc;

namespace ClimateStore.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credential()
        {
            //Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                Password = "secret",
                UserName = "admin",
            };
            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);
            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyURL");
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            // Arrange - create a mock authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);
            // Arrange - create the view model
            LoginViewModel model = new LoginViewModel
            {
                Password = "badPass",
                UserName = "badUser"
            };
            // Arrange - create the controller
            AccountController target = new AccountController(mock.Object);
            // Act - authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyURL");
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }
    }
}
