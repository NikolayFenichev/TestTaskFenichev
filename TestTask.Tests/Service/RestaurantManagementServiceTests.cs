using Moq;
using System.Threading.Tasks;
using TestTask.BLL.Dto;
using TestTask.BLL.Services;
using TestTask.Common;
using TestTask.DAL.Models;
using TestTask.DAL.Repositories;
using Xunit;

namespace TestTask.Tests.Service
{
    public class RestaurantManagementServiceTests
    {
        [Fact]
        public async Task AddCityAsync_AddCity_AddedCityReturned()
        {
            // Arrange
            var newCityDto = new CityDto() { Name = Common.CityName };
            var resultCityDto = new CityDto() { Id = 1, Name = Common.CityName };
            var resultCity = new City() { Id = 1, Name = Common.CityName };

            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<ICityRepository>();

            mockRepo.Setup(cityRepo => cityRepo.AddAsync(It.IsAny<City>()))
                .Returns(Task.FromResult(resultCity));
            mockUow.Setup(rmService => rmService.Cities)
                .Returns(mockRepo.Object);

            var svc = new RestaurantManagementService(mockUow.Object);

            // Act
            var result = await svc.AddCityAsync(newCityDto);

            // Assert
            Assert.Equal(resultCityDto, result);
        }

        [Fact]
        public async Task AddRestaurantAsync_AddRestaurant_AddedRestaurantReturned()
        {
            // Arrange
            var newRestaurantDto = 
                new RestaurantDto() { Name = Common.RestaurantName, CityId = Common.CityId };
            var resultRestaurantDto = 
                new RestaurantDto() { Id = 1, Name = Common.RestaurantName, CityId = Common.CityId };
            var resultRestaurant = 
                new Restaurant() { Id = 1, Name = Common.RestaurantName, CityId = Common.CityId };

            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRestaurantRepository>();

            mockRepo.Setup(cityRepo => cityRepo.AddAsync(It.IsAny<Restaurant>()))
                .Returns(Task.FromResult(resultRestaurant));
            mockUow.Setup(rmService => rmService.Restaurants)
                .Returns(mockRepo.Object);

            var svc = new RestaurantManagementService(mockUow.Object);

            // Act
            var result = await svc.AddRestaurantAsync(newRestaurantDto);

            // Assert
            Assert.Equal(resultRestaurantDto, result);
        }

        [Fact]
        public async Task GetRestaurantsByCityAsync_GetRestaurants_RestaurantsGetedWhithPagination()
        {
            // Arrange
            var pageParameters = new PageParameters()
            {
                PageNumber = Common.PageNumber,
                PageSize = Common.PageSize
            };

            var commonVariables = new Common();
            var resultRestaurants = commonVariables.Restaurants;
            var resultRestaurantsDto = commonVariables.RestaurantsDto;

            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRestaurantRepository>();

            mockRepo.Setup(cityRepo => 
                cityRepo.GetRestaurantsByCityAsync(It.IsAny<PageParameters>(), Common.CityId))
                .Returns(Task.FromResult(resultRestaurants));
            mockUow.Setup(rmService => rmService.Restaurants)
                .Returns(mockRepo.Object);

            var svc = new RestaurantManagementService(mockUow.Object);

            // Act
            var result = await svc.GetRestaurantsByCityAsync(pageParameters, Common.CityId);

            // Assert
            Assert.Equal(resultRestaurantsDto, result);
        }
    }
}
