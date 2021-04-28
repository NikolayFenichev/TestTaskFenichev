using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.BLL.Dto;
using TestTask.BLL.Services.Interfaces;
using TestTask.DAL.Models;
using TestTask.DAL.Repositories;

namespace TestTask.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IRestaurantManagementService _restaurantManagementService;

        public CitiesController(IRestaurantManagementService service)
        {
            _restaurantManagementService = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CityDto city)
        {
            if (ModelState.IsValid)
            {
                city = await _restaurantManagementService.AddCityAsync(city);

                var routeValue = new { city.Id, city.Name };

                return CreatedAtRoute(routeValue, city);
            }

            return BadRequest(ModelState);
        }
    }
}
