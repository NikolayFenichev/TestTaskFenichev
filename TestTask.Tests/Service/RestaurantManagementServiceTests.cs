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
            var newCityDto = new CityDto() { Name = CommonVariables.CityName };
            var resultCityDto = new CityDto() { Id = 1, Name = CommonVariables.CityName };
            var resultCity = new City() { Id = 1, Name = CommonVariables.CityName };

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
                new RestaurantDto() { Name = CommonVariables.RestaurantName, CityId = CommonVariables.CityId };
            var resultRestaurantDto = 
                new RestaurantDto() { Id = 1, Name = CommonVariables.RestaurantName, CityId = CommonVariables.CityId };
            var resultRestaurant = 
                new Restaurant() { Id = 1, Name = CommonVariables.RestaurantName, CityId = CommonVariables.CityId };

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
                PageNumber = CommonVariables.PageNumber,
                PageSize = CommonVariables.PageSize
            };

            var commonVariables = new CommonVariables();
            var resultRestaurants = commonVariables.Restaurants;
            var resultRestaurantsDto = commonVariables.RestaurantsDto;

            var mockUow = new Mock<IUnitOfWork>();
            var mockRepo = new Mock<IRestaurantRepository>();

            mockRepo.Setup(cityRepo => 
                cityRepo.GetRestaurantsByCityAsync(It.IsAny<PageParameters>(), CommonVariables.CityId))
                .Returns(Task.FromResult(resultRestaurants));
            mockUow.Setup(rmService => rmService.Restaurants)
                .Returns(mockRepo.Object);

            var svc = new RestaurantManagementService(mockUow.Object);

            // Act
            var result = await svc.GetRestaurantsByCityAsync(pageParameters, CommonVariables.CityId);

            // Assert
            Assert.Equal(resultRestaurantsDto, result);
        }
    }
}
