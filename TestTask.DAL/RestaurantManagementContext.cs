using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Models;

namespace TestTask.DAL
{
    public class RestaurantManagementContext: DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public RestaurantManagementContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
