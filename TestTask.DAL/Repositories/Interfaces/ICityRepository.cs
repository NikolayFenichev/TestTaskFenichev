using System.Threading.Tasks;
using TestTask.DAL.Models;

namespace TestTask.DAL.Repositories
{
    public interface ICityRepository: IBaseRepository<City>
    {
        Task<City> GetByIdAsync(int id);
    }
}
