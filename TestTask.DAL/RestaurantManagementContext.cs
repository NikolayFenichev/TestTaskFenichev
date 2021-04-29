using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Models;

namespace TestTask.DAL
{
    public class RestaurantManagementContext: DbContext
    {
        private readonly DbContextOptions _options;
        public DbSet<City> Cities { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public RestaurantManagementContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
