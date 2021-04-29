using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTask.BLL.Dto;
using TestTask.BLL.Services.Interfaces;

namespace TestTask.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IRestaurantManagementService _restaurantManagementService;
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(IRestaurantManagementService service,
            ILogger<CitiesController> logger)
        {
            _restaurantManagementService = service;
            _logger = logger;
        }

        /// <summary>
        /// Добавляет город
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "Name": "Чебоксары"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Город добавлен</response>
        /// <response code="400">Неверная модель в параметре</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CityDto city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    city = await _restaurantManagementService.AddCityAsync(city);

                    var routeValue = new { city.Id, city.Name };

                    return CreatedAtRoute(routeValue, city);
                }

                var message = "Неверная модель в параметре";
                _logger.LogError($"{nameof(Create)}:{city} - {message}");

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Create)}: {ex.Message}");
                throw;
            }
            
        }
    }
}
