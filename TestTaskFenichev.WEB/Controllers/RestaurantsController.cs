using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using TestTask.DAL.Models;
using TestTask.DAL.Repositories;

namespace TestTask.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public RestaurantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRestaurantsByCity([FromQuery] PageParameters pageParameters, int cityId)
        {
            var restaurants = await _unitOfWork.Cities.GetRestaurantsByCityAsync(pageParameters, cityId);

            if (restaurants == null)
            {
                return NotFound();
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

            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(restaurantPageInfo));

            return Ok(restaurants);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Restaurants.AddAsync(restaurant);

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
