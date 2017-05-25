using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartMarket.UI;
using SmartMarket.UI.Controllers;

namespace SmartMarket.UI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
