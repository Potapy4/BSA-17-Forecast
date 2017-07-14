using Moq;
using NUnit.Framework;
using System.Web.Mvc;
using WebForecast.BLL.Interfaces;
using WebForecastMVC.Controllers;

namespace WebForecast.TESTS
{
    [TestFixture]
    public class FavoriteCitiesControllerTests
    {
        private Mock<IBusinessLogic> logic;

        [SetUp]
        public void Initialize()
        {
            logic = new Mock<IBusinessLogic>();
        }

        [Test]
        public void FavoriteCitiesAddToFavorite_WithValiddParam_CallLogicMethodAndRedirectToIndex()
        {
            // Arrange
            FavoriteCitiesController favoriteCitiesController = new FavoriteCitiesController(logic.Object);
            logic.Setup(x => x.AddFavoriteCity(It.IsNotNull<string>())).Verifiable();

            // Act
            RedirectToRouteResult result = favoriteCitiesController.AddToFavorite("Test") as RedirectToRouteResult;

            // Assert
            logic.Verify(x => x.AddFavoriteCity(It.IsNotNull<string>()));
            Assert.IsTrue(result.RouteValues.ContainsKey("action"));
            Assert.AreEqual("Index", result.RouteValues["action"].ToString());
            Assert.IsNull(result.RouteValues["controller"]); // Must be null, because we in the same controller
        }
    }
}
