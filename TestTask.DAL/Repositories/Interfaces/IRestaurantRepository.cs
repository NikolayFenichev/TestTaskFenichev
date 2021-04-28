using System.Threading.Tasks;
using TestTask.Common;
using TestTask.DAL.Models;

namespace TestTask.DAL.Repositories
{
    public interface IRestaurantRepository: IBaseRepository<Restaurant>
    {
        Task<PagedList<Restaurant>> GetRestaurantsByCityAsync(PageParameters pageParameters, int cityId);
    }
}
