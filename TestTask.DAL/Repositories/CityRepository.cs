using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestTask.DAL.Models;

namespace TestTask.DAL.Repositories
{
    class CityRepository : ICityRepository
    {
        private RestaurantManagementContext _db;

        public CityRepository(RestaurantManagementContext db)
        {
            _db = db;
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _db.Cities.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<City> AddAsync(City city)
        {
            var result = _db.Cities.Add(city);
            await _db.SaveChangesAsync();

            return result.Entity;
        }
    }
}
