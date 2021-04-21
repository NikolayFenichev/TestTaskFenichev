using System.Threading.Tasks;
using TestTask.DAL.Models;

namespace TestTask.DAL.Repositories
{
    public interface ICityRepository: IBaseRepository<City>
    {
        Task<PagedList<Restaurant>> GetRestaurantsByCityAsync(PageParameters pageParameters, int cityId);
    }
}
