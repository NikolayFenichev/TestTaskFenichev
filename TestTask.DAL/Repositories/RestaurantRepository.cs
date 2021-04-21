using System.Threading.Tasks;
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

        public async Task AddAsync(Restaurant entity)
        {
            _db.Restaurants.Add(entity);
            await _db.SaveChangesAsync();
        }
    }
}
