using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest();
            }
            await _unitOfWork.Restaurants.AddAsync(restaurant);

            return Ok(restaurant);
        }
    }
}
