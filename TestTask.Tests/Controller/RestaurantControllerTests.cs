﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.BLL.Dto;
using TestTask.BLL.Services.Interfaces;
using TestTask.WEB.Controllers;
using TestTask.Common;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace TestTask.Tests.Controller
{
    public class RestaurantControllerTests
    {
        [Fact]
        public async Task Create_AddRestaurant_AddedRestaurantReturned()
        {
            // Arrange
            var newRestaurant = new RestaurantDto() { Name = CommonVariables.RestaurantName };
            var resultRestaurant = new RestaurantDto() { Id = 1, Name = CommonVariables.RestaurantName };

            var mock = new Mock<IRestaurantManagementService>();
            mock.Setup(rmService => rmService.AddRestaurantAsync(It.IsAny<RestaurantDto>()))
                .Returns(Task.FromResult(resultRestaurant));

            var controller = new RestaurantsController(mock.Object);

            // Act
            var result = await controller.Create(newRestaurant);

            // Assert
            var actionResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(resultRestaurant, actionResult?.Value);
        }

        [Fact]
        public async Task Create_AddRestaurantWithoutName_BadRequest()
        {
            // Arrange
            var newRestaurant = new RestaurantDto() { Name = null };

            var mock = new Mock<IRestaurantManagementService>();

            var controller = new RestaurantsController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await controller.Create(newRestaurant);

            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }

        [Fact]
        public async Task Create_AddRestaurantCityIdLessOne_BadRequest()
        {
            // Arrange
            var newRestaurant = new RestaurantDto() 
            { 
                Name = CommonVariables.RestaurantName, 
                CityId = CommonVariables.BadCityId 
            };

            var mock = new Mock<IRestaurantManagementService>();

            var controller = new RestaurantsController(mock.Object);
            controller.ModelState.AddModelError("CityId", "MustMoreZero");

            // Act
            var result = await controller.Create(newRestaurant);

            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }

        [Fact]
        public async Task GetRestaurantsByCity_CityIdMoreOne_ReturnedRestaurantsSpecificCityWithPagination()
        {
            // Arrange
            var pageParameters = new PageParameters() 
            { 
                PageNumber = CommonVariables.PageNumber, 
                PageSize = CommonVariables.PageSize 
            };
            var resultRestaurants = 
                new PagedList<RestaurantDto>(new CommonVariables().RestaurantsDto, CommonVariables.TotalCount, 
                    CommonVariables.PageNumber, CommonVariables.PageSize);

            var mock = new Mock<IRestaurantManagementService>();
            mock.Setup(rmService => 
                rmService.GetRestaurantsByCityAsync(It.IsAny<PageParameters>(), It.IsAny<int>()))
                .Returns(Task.FromResult(resultRestaurants));

            var response = new Mock<HttpResponse>();

            var httpContext = new DefaultHttpContext();
            httpContext.Response.Headers.Add("X-Test-Header","Test Header");

            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var controller = new RestaurantsController(mock.Object)
            {
                ControllerContext = controllerContext,
            };

            // Act
            var result = await controller.GetRestaurantsByCity(pageParameters, CommonVariables.CityId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(resultRestaurants, actionResult?.Value);
        }

        [Fact]
        public async Task GetRestaurantsByCity_CityIdLessOne_BadRequest()
        {
            // Arrange
            var pageParameters = new PageParameters()
            {
                PageNumber = CommonVariables.PageNumber,
                PageSize = CommonVariables.PageSize
            };

            var mock = new Mock<IRestaurantManagementService>();

            var controller = new RestaurantsController(mock.Object);

            // Act
            var result = await controller.GetRestaurantsByCity(pageParameters, CommonVariables.BadCityId);

            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }
    }
}