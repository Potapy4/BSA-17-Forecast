using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecastMVC.Controllers;

namespace WebForecast.TESTS
{
    [TestFixture]
    public class ForecastControllersTests
    {
        private Mock<IBusinessLogic> logic;

        [SetUp]
        public void Initialize()
        {
            logic = new Mock<IBusinessLogic>();
        }

        [Test]
        public void ForecastIndex_WithValidParams_ShouldCallAllActionChainsInController()
        {
            // Arrange
            ForecastController forecastContoller = new ForecastController(logic.Object);

            string city = "Lviv";
            int days = 7;

            #region WeatherObject
            var weather = new BLL.BusinessModels.OpenWeatherMap.Weather()
            {
                City = new BLL.BusinessModels.OpenWeatherMap.City()
                {
                    Id = 1,
                    Name = "Lviv"
                },

                List = new List<BLL.BusinessModels.OpenWeatherMap.WeatherDetails>()
                {
                    new BLL.BusinessModels.OpenWeatherMap.WeatherDetails()
                    {
                        Dt = 1499940000,
                        Temp = new BLL.BusinessModels.OpenWeatherMap.Temperature(){Day = 13, Min = 11.26, Max = 25.7, Night = 12.5, Eve = 6.11, Morn = 13},
                        Pressure = 989.92,
                        Humidity = 92,
                        Weather = new List<BLL.BusinessModels.OpenWeatherMap.WeatherInfo>()
                        {
                            new BLL.BusinessModels.OpenWeatherMap.WeatherInfo()
                            {
                                Id = 500,
                                Main = "Rain",
                                Description = "Light Rain",
                                Icon = "10d"
                            }
                        },
                        Speed = 11.62,
                        Deg = 109,
                        Clouds = 95,
                        Rain = 11.7
                    }
                }
            };
            #endregion

            logic.Setup(x => x.GetForecastAsync(It.IsNotNull<string>(), It.IsAny<int?>())).ReturnsAsync(weather);
            logic.Setup(x => x.LogIntoHistoryAsync(It.IsNotNull<HistoryDTO>())).Verifiable();

            // Act
            var result = forecastContoller.Index(city, days);

            // Assert
            Assert.IsNotNull(result);
            logic.Verify(x => x.LogIntoHistoryAsync(It.IsNotNull<HistoryDTO>()));
        }
    }
}
