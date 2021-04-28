using System.Linq;
using System.Threading.Tasks;
using TestTask.Common;
using TestTask.DAL.Models;

namespace TestTask.DAL.Repositories
{
    class RestaurantRepository : IRestaurantRepository
    {
        private RestaurantManagementContext _db;

        public RestaurantRepository(RestaurantManagementContext db)
        {
            _db = db;
        }

        public async Task<Restaurant> AddAsync(Restaurant restaurant)
        {
            var result = _db.Restaurants.Add(restaurant);
            await _db.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<PagedList<Restaurant>> GetRestaurantsByCityAsync(PageParameters pageParameters, int cityId)
        {
            var query = _db.Restaurants.Where(r => r.CityId == cityId);

            return await PagedListExtention<Restaurant>.ToPagedList(query, pageParameters.PageNumber, pageParameters.PageSize);
        }
    }
}
