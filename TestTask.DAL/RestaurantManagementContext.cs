using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Models;

namespace TestTask.DAL
{
    public class RestaurantManagementContext: DbContext
    {
        public RestaurantManagementContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=RestaurantManagementDb;Trusted_Connection=True;");
        }

        public DbSet<City> Cyties { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
