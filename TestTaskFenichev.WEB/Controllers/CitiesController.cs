using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] City city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            await _unitOfWork.Cities.AddAsync(city);

            var routeValue = new { city.Id, city.Name };

            return CreatedAtRoute(routeValue, city);
        }
    }
}
