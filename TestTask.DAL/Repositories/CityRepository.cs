using System.Threading.Tasks;
using TestTask.DAL.Models;
using System.Linq;

namespace TestTask.DAL.Repositories
{
    class CityRepository : ICityRepository
    {
        private RestaurantManagementContext _db;

        public CityRepository(RestaurantManagementContext db)
        {
            _db = db;
        }

        public async Task AddAsync(City entity)
        {
            _db.Cyties.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedList<Restaurant>> GetRestaurantsByCityAsync(PageParameters pageParameters, int cityId)
        {
            var query = _db.Restaurants.Where(r => r.CityId == cityId);

            if (!query.Any())
            {
                return null;
            }

            return await PagedList<Restaurant>.ToPagedList(query, pageParameters.PageNumber, pageParameters.PageSize);
        }
    }
}
