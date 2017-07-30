using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecast.BLL.Services;
using WebForecast.DAL.Entities;
using WebForecast.DAL.Interfaces;

namespace WebForecast.TESTS
{
    [TestFixture]
    public class BusinessLogicServiceTests
    {
        private Mock<IRepository<City>> cityRepository;
        private Mock<IRepository<History>> historyRepository;
        private Mock<IUnitOfWork> uofMock;
        private Mock<IForecastProvider> forcastProvider;
        private IBusinessLogic logic;

        [SetUp]
        public void Initialize()
        {
            cityRepository = new Mock<IRepository<City>>();
            historyRepository = new Mock<IRepository<History>>();

            uofMock = new Mock<IUnitOfWork>();
            forcastProvider = new Mock<IForecastProvider>();
            logic = new BusinessLogicService(uofMock.Object, forcastProvider.Object);

            // Arrange
            uofMock.Setup(x => x.FavoriteCities).Returns(cityRepository.Object);
            uofMock.Setup(x => x.History).Returns(historyRepository.Object);
        }

        [Test]
        public void AddFavoriteCity_WithNullParametr_ThrowArgumentNullException()
        {
            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => logic.AddFavoriteCity(null));
        }

        [Test]
        public void AddFavoriteCity_WithStringParametr_CallSaveMethod()
        {
            // Act
            logic.AddFavoriteCity("Test");

            // Assert
            uofMock.Verify(x => x.Save());
        }

        [Test]
        public void GetFavoriteCities_ShouldReturnCollectionWithThreeElements()
        {
            // Arrange
            IEnumerable<City> dataCity = new List<City>()
            {
                new City(){ Id = 1, Name = "Test 1"},
                new City(){ Id = 2, Name = "Test 2"},
                new City(){ Id = 3, Name = "Test 3"}
            };

            uofMock.Setup(x => x.FavoriteCities.GetAll()).Returns(dataCity);

            // Act
            IEnumerable<CityDTO> resultList = logic.GetFavoriteCities();

            // Assert
            CollectionAssert.IsNotEmpty(resultList);
            Assert.AreEqual(dataCity.Count(), resultList.Count());
        }

        [Test]
        public void GetFavoriteCityById_WithNegativeId_ShouldReturnNull()
        {
            // Arrange
            uofMock.Setup(x => x.FavoriteCities.Get(It.Is<int>(y => y < 0))).Returns((City)null);

            // Act
            CityDTO result = logic.GetFavoriteCityById(-1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetForecast_ForLvivNextSevenDays_ShouldCorrectWeatherInstance()
        {
            // Arrange
            string city = "Lviv";
            int days = 7;

            forcastProvider.Setup(x => x.GetForecast(city, days).Result).Returns(new BLL.BusinessModels.OpenWeatherMap.Weather()
            {
                City = new BLL.BusinessModels.OpenWeatherMap.City() { Name = city },
                List = new List<BLL.BusinessModels.OpenWeatherMap.WeatherDetails>(days)
            });

            // Act
            var weather = logic.GetForecast(city, days).Result;

            // Assert
            Assert.IsNotNull(weather);
            Assert.AreEqual(city, weather.City.Name);
            Assert.AreEqual(days, weather.List.Capacity);
        }

        [Test]
        public void LogIntoHistory_WithNullArgument_ReturnArgumentNullExceptionWithMessage()
        {
            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => logic.LogIntoHistory(null));
            Assert.AreEqual("History can't be null!", exception.ParamName);
        }

        [Test]
        public void EditFavoriteCity_WithValidParam_MakeChangeAndSave()
        {
            // Arrange
            CityDTO city = new CityDTO() { Id = 1, Name = "Test" };            

            uofMock.Setup(x => x.FavoriteCities.Update(It.IsNotNull<City>())).Verifiable();
            uofMock.Setup(x => x.Save()).Verifiable();

            // Act
            logic.EditFavoriteCity(city);

            //Assert
            uofMock.Verify(x => x.FavoriteCities.Update(It.IsNotNull<City>()));
            uofMock.Verify(x => x.Save());
        }
        
        [Test]
        public void DeleteFavoriteCity_WithNegativeParam_ShouldThrowArgumentExceptionWithMessage()
        {
            // Act and Assert
            var exception = Assert.Throws<ArgumentException>(() => logic.DeleteFavoriteCityAsync(-1));
            Assert.AreEqual("Id can't be negative!", exception.Message);
        }

        [Test]
        public void GetAllHistory_WithoutEntitiesInDb_ShouldReturnEmptyList()
        {
            // Act
            IEnumerable<HistoryDTO> historyList = logic.GetAllHistory();

            // Assert
            Assert.IsNotNull(historyList);
            Assert.IsEmpty(historyList);            
        }
    }
}
