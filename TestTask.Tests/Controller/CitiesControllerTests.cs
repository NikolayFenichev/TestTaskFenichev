using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using TestTask.BLL.Dto;
using TestTask.BLL.Services.Interfaces;
using TestTask.WEB.Controllers;
using Xunit;

namespace TestTask.Tests.Controller
{
    public class CitiesControllerTests
    {
        [Fact]
        public async Task Create_AddCity_AddedCityReturned()
        {
            // Arrange
            var newCity = new CityDto() { Name = Common.CityName };
            var resultCity = new CityDto() { Id = 1, Name = Common.CityName };

            var mock = new Mock<IRestaurantManagementService>();
            mock.Setup(rmService => rmService.AddCityAsync(It.IsAny<CityDto>()))
                .Returns(Task.FromResult(resultCity));

            var loggerMock = new Mock<ILogger<CitiesController>>();

            var controller = new CitiesController(mock.Object, loggerMock.Object);

            // Act
            var result = await controller.Create(newCity);

            // Assert
            var actionResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(resultCity, actionResult?.Value);
        }

        [Fact]
        public async Task Create_AddCityWithoutName_BadRequest()
        {
            // Arrange
            var newCity = new CityDto() { Name = null };

            var mock = new Mock<IRestaurantManagementService>();
            var loggerMock = new Mock<ILogger<CitiesController>>();

            var controller = new CitiesController(mock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await controller.Create(newCity);

            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }
    }
}
