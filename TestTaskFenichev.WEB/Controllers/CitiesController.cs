using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestTask.DAL.Models;
using TestTask.DAL.Repositories;

namespace TestTask.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurantsByCity([FromQuery] PageParameters pageParameters, int cityId)
        {
            var restaurants = await _unitOfWork.Cities.GetRestaurantsByCityAsync(pageParameters, cityId);

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
        public async Task<IActionResult> Create([FromBody] City city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            await _unitOfWork.Cities.AddAsync(city);

            return Ok(city);
        }
    }
}
