using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Models;

namespace TestTask.DAL
{
    public class RestaurantManagementContext: DbContext
    {
        public RestaurantManagementContext()
        {

        }

        public RestaurantManagementContext(DbContextOptions options): base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
    }
}
