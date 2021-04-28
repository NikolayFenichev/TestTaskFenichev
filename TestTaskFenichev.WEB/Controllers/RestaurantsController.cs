using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TestTask.BLL.Dto;
using TestTask.BLL.Services.Interfaces;
using TestTask.Common;

namespace TestTask.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private IRestaurantManagementService _restaurantManagementService;

        public RestaurantsController(IRestaurantManagementService service)
        {
            _restaurantManagementService = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRestaurantsByCity([FromQuery] PageParameters pageParameters, int cityId)
        {
            if (cityId < 1)
            {
                return BadRequest("Идентификартор города должен быть больше 0");
            }
            var restaurants = await _restaurantManagementService.GetRestaurantsByCityAsync(pageParameters, cityId);

            var restaurantPageInfo = new
            {
                restaurants.TotalCount,
                restaurants.PageSize,
                restaurants.CurrentPage,
                restaurants.TotalPages,
                restaurants.HasNext,
                restaurants.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(restaurantPageInfo));

            return Ok(restaurants);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RestaurantDto restaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            restaurant = await _restaurantManagementService.AddRestaurantAsync(restaurant);

            var routeValue = new
            {
                restaurant.Id,
                restaurant.Name,
                restaurant.CityId
            };

            return CreatedAtRoute(routeValue, restaurant);
        }
    }
}
