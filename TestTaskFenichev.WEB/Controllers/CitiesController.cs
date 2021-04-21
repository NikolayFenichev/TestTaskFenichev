using System.Threading.Tasks;
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
