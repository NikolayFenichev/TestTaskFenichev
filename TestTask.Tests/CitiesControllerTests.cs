using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.DAL.Models;
using TestTask.DAL.Repositories;
using TestTask.WEB.Controllers;
using Xunit;

namespace TestTask.Tests
{
    public class CitiesControllerTests
    {
        [Fact]
        public async Task Create_AddCity_AddedCityPresentInTestsCities()
        {
            // Arrange
            City newCity = new City() { Name = "Хабаровск" };
            var mock = new Mock<UnitOfWork>();
            mock.Setup(uow => uow.Cities.AddAsync(It.IsAny<City>()))
                .Returns(Task.CompletedTask);
            var controller = new CitiesController(mock.Object);

            // Act
            var result = await controller.Create(newCity);

            // Assert
            var actionResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(newCity, actionResult?.Value);
        }

        private IEnumerable<City> GetTestCities()
        {
            return new List<City>() 
            {
                new City() {
                    Id = 1,
                    Name = "Нижний-Новгород"
                },
                new City() {
                    Id = 2,
                    Name = "Москва"
                },
                new City() {
                    Id = 3,
                    Name = "Казань"
                },
                new City() {
                    Id = 4,
                    Name = "Рязань"
                }
            };
        }
    }
}
