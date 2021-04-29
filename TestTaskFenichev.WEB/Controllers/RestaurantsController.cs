using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
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
        private readonly ILogger<RestaurantsController> _logger;

        public RestaurantsController(IRestaurantManagementService service,
            ILogger<RestaurantsController> logger)
        {
            _restaurantManagementService = service;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает рестораны указанного города
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "PageNumber": 1,
        ///        "PageSize": 2,
        ///        "cityId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="pageParameters">Параметры страницы</param>
        /// <param name="cityId">Идентификатор города</param>
        /// <response code="200">Рестораны найдены</response>
        /// <response code="400">Неверный идентификатор города</response>
        /// <response code="404">Рестораны не найдены</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRestaurantsByCity([FromQuery] PageParameters pageParameters, int cityId)
        {
            if (cityId < 1)
            {
                var message = "Идентификартор города должен быть больше 0";
                _logger.LogError($"{nameof(GetRestaurantsByCity)}: {message}");

                return BadRequest(message);
            }

            try
            {
                var restaurants = await _restaurantManagementService.GetRestaurantsByCityAsync(pageParameters, cityId);

                if (!restaurants.Any())
                {
                    var message = "Рестораны не найдены";
                    _logger.LogError($"{nameof(GetRestaurantsByCity)}: {message}");

                    return NotFound(message);
                }

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
            catch(Exception ex)
            {
                _logger.LogError($"{nameof(GetRestaurantsByCity)}: {ex.Message}");
                throw;
            }
            
        }

        /// <summary>
        /// Добавляет ресторан в указанный город
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "Name": "У Реки",
        ///        "cityId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Ресторан добавлен</response>
        /// <response code="400">Неверная модель в параметре</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RestaurantDto restaurant)
        {
            if (!ModelState.IsValid)
            {
                var message = "Неверная модель в параметре";
                _logger.LogError($"{nameof(Create)}:{restaurant} - {message}");

                return BadRequest(ModelState);
            }

            try
            {
                restaurant = await _restaurantManagementService.AddRestaurantAsync(restaurant);

                var routeValue = new
                {
                    restaurant.Id,
                    restaurant.Name,
                    restaurant.CityId
                };

                return CreatedAtRoute(routeValue, restaurant);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Create)}: {ex.Message}");
                throw;
            }
            
        }
    }
}
